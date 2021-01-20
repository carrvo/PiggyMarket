Describe "Configuration to customize experience" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "Bank Account Group" -Skip {
        It "create Bank Account Group" -Tags "vB.2.0" {
            # Requirement
            New-BankGroup -AccessToken $token -Name "MyAccountGroup" `
                | Should -Exist
            Get-BankGroup -AccessToken $token `
                | Should -HaveCount 1
        }
        It "update Bank Account Group" -Tags "vB.2.1" {
            # Pre-Requisite
            New-BankGroup -AccessToken $token -Name "MyAccountGroup"

            # Requirement
            Get-BankGroup -AccessToken $token -Name "MyAccountGroup" `
                | Update-BankGroup -Name "MyNewGroupName"

            Get-BankGroup -AccessToken $token -Name "MyNewGroupName" `
                | Should -Exist
        }
        It "delete Bank Account Group" -Tags "vB.2.0" {
            # Pre-Requisite
            New-BankGroup -AccessToken $token -Name "MyAccountGroup"

            # Requirement
            Get-BankGroup -AccessToken $token -Name "MyAccountGroup" `
                | Remove-BankGroup
            Get-BankGroup -AccessToken $token -Name "MyAccountGroup" `
                | Should -Not -Exist
        }
    }

    Context "Bank Accounts with Groups" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "create Bank Account with Group" -Tags "vB.2.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar -Group "MyAccountGroup"
            Get-BankGroup -AccessToken $token -Name "MyAccountGroup" `
                | Should -Exist
        }
        It "updates Bank Account with Group" -Tags "vB.2.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Update-BankAccount -Group "MyAccountGroup"
            Get-BankGroup -AccessToken $token -Name "MyAccountGroup" `
                | Should -Exist
        }
        It "list Bank Account by Group" -Tags "vB.2.1" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar -Group "MyAccountGroup"

            # Requirement
            Get-BankAccount -AccessToken $token -Group "MyAccountGroup" `
                | Should -HaveCount 1
        }
        It "view Group Balance" -Tags "vB.2.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar -Group "MyAccountGroup"

            # Requirement
            Get-BankAccount -AccessToken $token -Group "MyAccountGroup" `
                | Measure-Object CurrentFunds -Sum `
                | Select-Object -ExpandProperty Sum
                | Should -BeGreaterThan 0
        }
        It "view Transactions by Group" -Tags "vB.2.2" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar -Group "MyAccountGroup"
            Add-SubCategory -AccessToken $token -CategoryName "Bills" -Name "GasBills"
            Add-Transaction -AccessToken $token -Payee GasCompany -SubCategory "GasBills" -BankAccount "MyAccount" -Price -100 -Currency CanadianDollar -ReceiptID Gas

            # Requirement
            Get-Transactions -BankGroup "MyAccountGroup" `
                | Should -HaveCount 1
        }
    }
}
