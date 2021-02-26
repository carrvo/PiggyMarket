Describe "Aggregates and Verifies data" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "viewing Transactions" {
        It "views Transactions by SubCategory" -Tags "TRK-VEW-02","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -SubCategory "GasBills" |
                Should -HaveCount 1
        }
        It "views Transactions by Category" -Tags "TRK-VEW-03","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Category "Bills" |
                Should -HaveCount 1
        }
        It "views Transactions by Payee" -Tags "TRK-VEW-03","v0.1.0","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Payee "GasCompany" |
                Should -HaveCount 1
        }
        It "views Transactions by Price range" -Tags "TRK-VEW-04","v1.1.3","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Minimum 1 -Maximum 1000 |
                Should -HaveCount 1
        }
        It "views Transactions by Posted Date range" -Tags "TRK-VEW-05","v0.1.3","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"
            $yesterday = Get-Date |
                ForEach-Object AddDays -1
            $tomorrow = Get-Date |
                ForEach-Object AddDays +1

            # Requirement
            Get-Transaction -AccessToken $token -PostedOnOrAfter $yesterday -PostedOnOrBefore $tomorrow |
                Should -HaveCount 1
        }
        It "views Transactions by Tags" -Tags "TRK-VEW-06","v0.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Tags "Refundable"

            # Requirement
            Get-Transaction -AccessToken $token -Tags "Refundable" |
                Should -HaveCount 1
        }
        It "views all Transations" -Tags "TRK-VEW-07","v0.1.0","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token |
                Should -HaveCount 1
        }
    }

    Context "With Bank Account" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "views Transations by Verified" -Tags "TRK-BNK-01","vB.1.2","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -NotVerified |
                Should -HaveCount 1
        }
        It "verifies Transaction through Bank Account" -Tags "TRK-BNK-02","vB.1.3","Complex" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Debit -Account "MyAccount" -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" |
                Confirm-Transaction

            # Rationale
            <#
            This requirement allows transactions to be verified by two
            sources of authority: the recorded transaction and the bank.
            Here, consistency is important for detecting unauthorized
            purchases or transfers. The application should be able to
            correlate the transaction present in the system with a new
            transaction appearing in the bank account, within a limited
            time-frame (otherwise notify of potential unauthorized).
            #>
        }
        It "views Transactions by Processed Date range" -Tags "TRK-BNK-03","vB.1.3","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" |
                Confirm-Transaction
            $yesterday = Get-Date |
                ForEach-Object AddDays -1
            $tomorrow = Get-Date |
                ForEach-Object AddDays +1

            # Requirement
            Get-Transaction -AccessToken $token -ProcessedOnOrAfter $yesterday -ProcessedOnOrBefore $tomorrow |
                Should -HaveCount 1
        }
    }
}
