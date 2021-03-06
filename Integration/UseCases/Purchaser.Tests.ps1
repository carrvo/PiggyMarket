Describe "Ad-hoc use that Generates data and Corrects errors" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    It "supports updating in-memory object" -Tag "PCH-BAK-01","v0.0.3","Simple" {
        Get-Category -AccessToken $token -Name "Bills" | Update-Memory
    }

    Context "making Transactions" {
        It "makes cash purchase" -Tag "PCH-TRN-01","v0.1.0","Simple" {
            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -UnCategorized -Cash -Price -200 -Currency CanadianDollar -NoReceipt |
                Should -Exist
        }
        It "makes cash purchase with SubCategory" -Tag "PCH-TRN-02","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -NoReceipt |
                Should -Exist
        }
        It "makes cash purchase with Receipt" -Tag "PCH-TRN-02","v0.1.2","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" |
                Should -Exist
        }
        It "makes cash purchase with Tag" -Tag "PCH-TRN-03","v0.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Tags "Refundable" |
                Should -Exist
        }
        It "makes cash purchase with Comment" -Tag "PCH-TRN-04","v0.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Comment "Paid in full." |
                Should -Exist
        }
        It "makes cash purchase, adjusted date" -Tag "PCH-TRN-05","v0.1.3","Complex" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            $yesterday = Get-Date |
                ForEach-Object AddDays -1

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Posted $yesterday |
                Should -Exist
        }
        It "supports cheque purchases" -Tag "PCH-TRN-06","v0.1.0","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cheque -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" |
                Should -Exist
        }
        It "supports credit purchases" -Tag "PCH-TRN-07","v0.1.0","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Credit -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" |
                Should -Exist
        }
        It "supports debit purchases" -Tag "PCH-TRN-08","v0.1.0","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Debit -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" |
                Should -Exist
        }
        It "move funds to Goal" -Tag "PCH-GOL-01","v1.1.1","Simple" {
            # Pre-Requisite
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly

            # Requirement
            Add-ToGoal -AccessToken $token -Name "Save for Theatre" -Goal "Theatre" -Cash -Price -10 -Currency CanadianDollar -ReceiptID "TheatreSavingsReceipt" |
                Should -Exist

            # Rationale
            <#
            Movement is not truly a transaction or a transfer, but the system will treat it as a transaction.
            #>
        }
        It "completes Goal" -Tag "PCH-GOL-02","v1.1.1","Moderate" {
            # Pre-Requisite
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly
            Add-ToGoal -AccessToken $token -Name "Save for Theatre" -Goal "Theatre" -Cash -Price -120 -Currency CanadianDollar -ReceiptID "TheatreSavingsReceipt"

            # Requirement
            Get-Goal -AccessToken $token -Name "Theatre" |
                Confirm-Goal
            New-Transaction -AccessToken $token -Name "Watch Theatre" -Payee "TheatreCompany" -Goal "Theatre" -Cash -Price -120 -Currency CanadianDollar -ReceiptID "TheatreReceipt"
            Get-Goal -AccessToken $token -Name "Theatre" |
                Select-Object -ExpandProperty Complete |
                Should -Be $true
            Get-SubCategory -AccessToken $token -Name "Theatre" |
                Should -Not -Exist
        }
    }

    Context "viewing Transactions" {
        It "views Transactions by Name" -Tag "PCH-VEW-01","v0.1.0","Simple" {
            # Pre-Requisite
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -UnCategorized -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Should -HaveCount 1
        }
    }

    Context "modifying Transations" {
        It "modifies Name" -Tag "PCH-MOD-01","v0.1.0","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Edit-Transaction -Name "Electric Bill" |
                Select-Object -ExpandProperty Name |
                Should -Be "Electric Bill"
        }
        It "modifies SubCategory" -Tag "PCH-MOD-02","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Edit-Transaction -SubCategory "ElectricBills" |
                Select-Object -ExpandProperty SubCategory |
                Should -Be "ElectricBills"
        }
        It "adds Tags" -Tag "PCH-MOD-03","v0.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Add-Tag -Tags "Refundable" |
                Select-Object -ExpandProperty Tags |
                Should -Contain "Refundable"
        }
        It "removes Tags" -Tag "PCH-MOD-04","v0.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Tags "Refundable"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Remove-Tag -Tags "Refundable" |
                Select-Object -ExpandProperty Tags |
                Should -Not -Contain "Refundable"
        }
        It "modifies Comments" -Tag "PCH-MOD-05","v0.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Edit-Transaction -Comment "Paid in full." |
                Select-Object -ExpandProperty Comment |
                Should -Be "Paid in full."
        }
        It "Splits Transaction" -Tag "PCH-MOD-06","v0.2.0","Complex" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Split-Transaction -Name "GasSplit" -UnCategorized -Price -20
            Get-Transaction -AccessToken $token |
                Should -HaveCount 2
        }
        It "Splits Transaction with SubCategory" -Tag "PCH-MOD-07","v0.2.0","Complex" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Split-Transaction -Name "GasSplit" -SubCategory "GasBills" -Price -20
            Get-Transaction -AccessToken $token |
                Should -HaveCount 2
        }
        It "Splits Transaction with Tags" -Tag "PCH-MOD-08","v0.2.1","Complex" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Split-Transaction -Name "GasSplit" -SubCategory "GasBills" -Price -20 -Tags "Refundable"
            Get-Transaction -AccessToken $token |
                Should -HaveCount 2
        }
        It "Splits Transaction with Comment" -Tag "PCH-MOD-09","v0.2.1","Complex" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Split-Transaction -Name "GasSplit" -SubCategory "GasBills" -Price -20 -Comment "splitting"
            Get-Transaction -AccessToken $token |
                Should -HaveCount 2
        }
    }

    Context "other Day-To-Day Operations" {
        It "spends from Category" -Tag "PCH-DOP-01","v0.3.2","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Budget -AccessToken $token -Category "Bills" -Target 300 -Currency CanadianDollar -Period Monthly

            $before = Get-Category -AccessToken $token -Name "Bills" |
                Select-Object -ExpandProperty CurrentFunds
            $transPrice = -20

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price $transPrice -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"
            $after = Get-Category -AccessToken $token -Name "Bills" |
                Select-Object -ExpandProperty CurrentFunds
            ($after - $before) |
                Should -Be $transPrice
        }
    }

    Context "With Bank Account" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "supports Bank Account purchases" -Tag "PCH-BNK-01","vB.1.0","Moderate" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Debit -Account "MyAccount" -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" |
                Should -Exist
        }
        It "supports Transfers Between Accounts" -Tag "PCH-BNK-02","vB.1.1","Complex" {
            # Pre-Requisite
            $amount = 500
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Income" -Name "Paycheque"
            New-Transaction -AccessToken $token -Name "PAYCHEQUE" -Payee "Employer" -SubCategory "Paycheque" -Debit -Price (2 * $amount) -Currency CanadianDollar -ReceiptID "Paystub"
            $startingFunds = Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Select-Object -ExpandProperty CurrentFunds
            Add-BankAccount -AccessToken $token -Name "SecondayAccount" -Bank "MyBank" -Number 1234568 -Currency CanadianDollar

            # Requirement
            Add-Transfer -From "MyAccount" -To "SecondayAccount" -Amount $amount -Currency CanadianDollar
            $endingFunds = Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Select-Object -ExpandProperty CurrentFunds
            ($endingFunds - $startingFunds) |
                Should -Be -$amount
            Get-BankAccount -AccessToken $token -Name "SecondayAccount" |
                Select-Object -ExpandProperty CurrentFunds |
                Should -Be $amount
        }
    }
}
