Describe "Administration of Users and Access" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "Categories" {
        It "view Category" -Tags "v0.1.1" {
            # Requirement
            Get-Category -AccessToken $token -Name "Bills" `
                | Should -Exist
        }
        It "view all Categories" -Tags "v0.1.1" {
            # Requirement
            Get-Category -AccessToken $token `
                | Should -HaveCount 20 # TODO: choose #

            # Rationale
            <#
            Must be of the specified number.
            #>
        }
        It "create SubCategory" -Tags "v0.1.1" {
            # Requirement
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills" `
                | Should -Exist
        }
        It "view SubCategory" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" `
                | Should -Exist
        }
        It "view Category that SubCategory belongs to" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" `
                | Select-Object -ExpandProperty Category
                | Select-Object -ExpandProperty Name
                | Should -Be "Bills"
        }
        It "multiple SubCategories have same name" -Tags "v0.1.1" {
            # Requirement
            New-SubCategory -AccessToken $token -Category "Bills" -Name "Subscription"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            Get-SubCategory -AccessToken $token -Name "Subscription" `
                | Should -HaveCount 2
        }
        It "view SubCategories by Category" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            # Requirement
            Get-SubCategory -AccessToken $token -Category "Bills" `
                | Should -HaveCount 2
        }
        It "view all SubCategories" -Tags "v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            # Requirement
            Get-SubCategory -AccessToken $token `
                | Should -HaveCount 3
        }
        It "view remaining Funds" -Tags "v0.3.0" {
            # Requirement
            Get-Category -AccessToken $token -Name "Bills" `
                | Select-Object -ExpandProperty CurrentFunds `
                | Should -Be 0
        }
        It "view total remaining Funds" -Tags "v0.3.0" {
            # Requirement
            Get-Category -AccessToken $token `
                | Measure-Object CurrentFunds -Sum `
                | Select-Object -ExpandProperty Sum `
                | Should -Be 0
        }
        It "view remaining Funds from SubCategory" -Tags "v0.3.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" `
                | Select-Object -ExpandProperty Category
                | Select-Object -ExpandProperty CurrentFunds
                | Should -Be 0
        }
    }

    Context "Budgeting" {
        It "create Budget by Category" -Tags "v0.3.2" {
            # Requirement
            New-Budget -AccessToken $token -Category Bills -Target 500 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
        }
        It "create Budget by SubCategory" -Tags "v0.3.3" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Budget -AccessToken $token -SubCategory "GasBills" -Target 200 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
        }
        It "create Budget by Transaction name" -Tags "v0.3.3" {
            # Requirement
            New-Budget -AccessToken $token -TransactionName "GAS COMPANY - Bill" -Target 200 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
            # Requirement - parameter alias
            New-Budget -AccessToken $token -ItemName "ELECTRIC COMPANY - Bill" -Target 200 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
        }
    }

    Context "App Access" -Skip {
        It "invite other Users to share Access with" -Tags "v0.4.0" {
            # Requirement
        }
        It "identify what Accessing" -Tags "v0.4.1" {
            # Requirement
        }
        It "switch between Accessing" -Tags "v0.4.2" {
            # Requirement
        }
    }

    Context "Bank Account Synchronization" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "add Chequing Account from a Bank" -Tags "vB.1.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyChequingAccount" -Bank "MyBank" -Number 1234561 -Currency CanadianDollar `
                | Should -Exist
        }
        It "add eSavings Account from a Bank" -Tags "vB.1.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyESavingsAccount" -Bank "MyBank" -Number 1234562 -Currency CanadianDollar `
                | Should -Exist
        }
        It "add Credit Card from a Bank" -Tags "vB.1.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyCreditCard" -Bank "MyBank" -Number 1234563 -Currency CanadianDollar `
                | Should -Exist
        }
        It "lists and filters Bank Accounts" -Tags "vB.1.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount1" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            Add-BankAccount -AccessToken $token -Name "MyAccount2" -Bank "MyBank" -Number 1234568 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token `
                | Should -HaveCount 2
            Get-BankAccount -AccessToken $token `
                | Select-Object -ExpandProperty Name `
                | Should -Contain "MyAccount2"
            Get-BankAccount -AccessToken $token -Bank "MyBank" `
                | Should -HaveCount 2
        }
        It "view Bank Account details" -Tags "vB.1.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty Bank
                | Should -Be "MyBank"
        }
        It "view Credit Limit" -Tags "vB.1.3" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            Add-BankAccount -AccessToken $token -Name "MyCreditCard" -Bank "MyBank" -Number 1234563 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty CreditLimit `
                | Should -Not -Exist
            Get-BankAccount -AccessToken $token -Name "MyCreditCard" `
                | Select-Object -ExpandProperty CreditLimit `
                | Should -BeGreaterThan 0
        }
        It "update Bank Account details" -Tags "vB.1.2" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Update-BankAccount -BankName "MyAlternateBank" -Number 1234557
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Update-BankAccount -Currency CanadianDollar
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Update-BankAccount -Name "MyNewName"

            Get-BankAccount -AccessToken $token -Name "MyNewName" `
                | Select-Object -ExpandProperty Bank
                | Should -Be "MyAlternateBank"
        }
        It "delete Bank Account" -Tags "vB.1.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Remove-BankAccount
        }
    }

    Context "View Account Content" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "view single Balance" -Tags "vB.2.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty CurrentFunds `
                | Should -BeGreaterThan 0
        }
        It "view total Balance" -Tags "vB.2.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Measure-Object CurrentFunds -Sum `
                | Select-Object -ExpandProperty Sum
                | Should -BeGreaterThan 0
        }
        It "view Transactions" -Tags "vB.1.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -BankAccount "MyAccount" -Price -100 -Currency CanadianDollar -ReceiptID Gas

            # Requirement
            Get-Transactions -BankAccount "MyAccount" `
                | Should -HaveCount 1
        }
    }
}
