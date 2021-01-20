Describe "Aggregates and Verifies data" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "viewing Transactions" {
        It "views Transactions by SubCategory" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -SubCategory "GasBills" `
                | Should -HaveCount 1
        }
        It "views Transactions by Category" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Category "Bills" `
                | Should -HaveCount 1
        }
        It "views Transactions by Payee" -Tags "v0.1.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Payee "GasCompany" `
                | Should -HaveCount 1
        }
        It "views Transactions by Price range" -Tags "v1.1.3" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -Minimum 1 -Maximum 1000 `
                | Should -HaveCount 1
        }
        It "views Transactions by Posted Date range" -Tags "v0.1.3" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
            $yesterday = Get-Date `
                | ForEach-Object AddDays -1
            $tomorrow = Get-Date `
                | ForEach-Object AddDays +1

            # Requirement
            Get-Transaction -AccessToken $token -PostedOnOrAfter $yesterday -PostedOnOrBefore $tomorrow `
                | Should -HaveCount 1
        }
        It "views Transactions by Tags" -Tags "v0.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" -Tags "Refundable" `

            # Requirement
            Get-Transaction -AccessToken $token -Tags "Refundable" `
                | Should -HaveCount 1
        }
        It "views all Transations" -Tags "v0.1.0" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token `
                | Should -HaveCount 1
        }
    }

    Context "With Bank Account" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "views Transations by Verified" -Tags "vB.1.2" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `

            # Requirement
            Get-Transaction -AccessToken $token -NotVerified `
                | Should -HaveCount 1
        }
        It "verifies Transaction through Bank Account" -Tags "vB.1.3" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Debit -Account "MyAccount" -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt"

            # Requirement
            Get-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" `
                | Confirm-Transaction

            # Rationale
        }
        It "views Transactions by Processed Date range" -Tags "vB.1.3" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -Cash -Price -200 -Currency CanadianDollar -ReceiptID "GasCompanyReceipt" `
                | Confirm-Transaction
            $yesterday = Get-Date `
                | ForEach-Object AddDays -1
            $tomorrow = Get-Date `
                | ForEach-Object AddDays +1

            # Requirement
            Get-Transaction -AccessToken $token -ProcessedOnOrAfter $yesterday -ProcessedOnOrBefore $tomorrow `
                | Should -HaveCount 1
        }
    }
}
