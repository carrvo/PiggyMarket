Describe "Ad-hoc use that Generates data and Corrects errors" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    It "supports updating in-memory object" -Tags "v0.0.3" {
        Get-Category -AccessToken $token -Name "Bills" | Update-Memory
    }

    Context "making Transactions" {
        It "makes cash purchase" -Tags "v0.1.0" {
            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -UnCategorized -Cash -Price -200 -Currency CanadianDollar -NoReceipt `
                | Should -Exist
        }
        It "makes cash purchase with SubCategory" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -NoReceipt `
                | Should -Exist
        }
        It "makes cash purchase with Receipt" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
                | Should -Exist
        }
        It "makes cash purchase with Tag" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Tags "Refundable" `
                | Should -Exist
        }
        It "makes cash purchase with Comment" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Comment "Paid in full." `
                | Should -Exist
        }
        It "makes cash purchase, adjusted date" -Tags "v0.1.3" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            $yesterday = Get-Date `
                | ForEach-Object AddDays -1

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Posted $yesterday `
                | Should -Exist
        }
        It "supports cheque purchases" -Tags "v0.1.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cheque -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
                | Should -Exist
        }
        It "supports credit purchases" -Tags "v0.1.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Credit -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
                | Should -Exist
        }
        It "supports debit purchases" -Tags "v0.1.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Debit -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
                | Should -Exist
        }
    }

    Context "viewing Transactions" {
        It "views Transactions by Name" -Tags "v0.1.0" {
            # Pre-Requisite
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -UnCategorized -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Should -HaveCount 1
        }
    }

    Context "modifying Transations" {
        It "modifies Name" -Tags "v0.1.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Edit-Transaction -Name "Electric Bill" `
                | Select-Object -ExpandProperty Name `
                | Should -Be "Electric Bill"
        }
        It "modifies SubCategory" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Edit-Transaction -SubCategory "ElectricBills" `
                | Select-Object -ExpandProperty SubCategory `
                | Should -Be "ElectricBills"
        }
        It "adds Tags" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Add-Tag -Tags "Refundable" `
                | Select-Object -ExpandProperty Tags `
                | Should -Contain "Refundable"
        }
        It "removes Tags" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Tags "Refundable" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Remove-Tag -Tags "Refundable" `
                | Select-Object -ExpandProperty Tags `
                | Should -Not -Contain "Refundable"
        }
        It "modifies Comments" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Edit-Transaction -Comment "Paid in full." `
                | Select-Object -ExpandProperty Comment `
                | Should -Be "Paid in full."
        }
        It "Splits Transaction" -Tags "v0.2.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Split-Transaction -Name "GasSplit" -UnCategorized -Price -20
            Get-Transaction -AccessToken $token `
                | Should -HaveCount 2
        }
        It "Splits Transaction with SubCategory" -Tags "v0.2.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Split-Transaction -Name "GasSplit" -SubCategory "GasBills" -Price -20
            Get-Transaction -AccessToken $token `
                | Should -HaveCount 2
        }
        It "Splits Transaction with Tags" -Tags "v0.2.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Split-Transaction -Name "GasSplit" -SubCategory "GasBills" -Price -20 -Tags "Refundable"
            Get-Transaction -AccessToken $token `
                | Should -HaveCount 2
        }
        It "Splits Transaction with Comment" -Tags "v0.2.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Split-Transaction -Name "GasSplit" -SubCategory "GasBills" -Price -20 -Comment "splitting"
            Get-Transaction -AccessToken $token `
                | Should -HaveCount 2
        }
    }

    Context "other Day-To-Day Operations" {
        It "spends from Category" -Tags "v0.3.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Budget -AccessToken $token -Category "Bills" -Target 300 -Currency CanadianDollar -Period Monthly

            $before = Get-Category -AccessToken $token -Name "Bills" `
                | Select-Object -ExpandProperty CurrentFunds
            $transPrice = -20

            # Requirement
            Add-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price $transPrice -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"
            $after = Get-Category -AccessToken $token -Name "Bills" `
                | Select-Object -ExpandProperty CurrentFunds
            ($after - $before) `
                | Should -Be $transPrice
        }
    }

    Context "With Bank Account" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "supports Bank Account purchases" -Tags "vB.1.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Debit -Account "MyAccount" -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
                | Should -Exist
        }
        It "supports Transfers Between Accounts" -Tags "vB.1.1" {
            # Pre-Requisite
            $amount = 500
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Income" -Name "Paycheque"
            New-Transaction -AccessToken $token -Name "PAYCHEQUE" -Payee "Employer" -SubCategory "Paycheque" -Debit -Price (2 * $amount) -Currency CanadianDollar -ReceiptID "Paystub"
            $startingFunds = Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty CurrentFunds
            Add-BankAccount -AccessToken $token -Name "SecondayAccount" -Bank "MyBank" -Number 1234568 -Currency CanadianDollar

            # Requirement
            Add-Transfer -From "MyAccount" -To "SecondayAccount" -Amount $amount -Currency CanadianDollar
            $endingFunds = Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty CurrentFunds
            ($endingFunds - $startingFunds) `
                | Should -Be -$amount
            Get-BankAccount -AccessToken $token -Name "SecondayAccount" `
                | Select-Object -ExpandProperty CurrentFunds `
                | Should -Be $amount
        }
    }
}
