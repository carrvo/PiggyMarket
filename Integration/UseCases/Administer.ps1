Describe "Administration of Users and Access" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "Categories" {
        It "view Category" -Tags "ADM-CAT-01","v0.1.1" {
            # Requirement
            Get-Category -AccessToken $token -Name "Bills" `
                | Should -Exist
        }
        It "view all Categories" -Tags "ADM-CAT-02","v0.1.1" {
            # Requirement
            Get-Category -AccessToken $token `
                | Should -HaveCount 20 # TODO: choose #

            # Rationale
            <#
            Must be of the specified number.
            #>
        }
        It "create SubCategory" -Tags "ADM-CAT-03","v0.1.1" {
            # Requirement
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills" `
                | Should -Exist
        }
        It "view SubCategory" -Tags "ADM-CAT-04","v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" `
                | Should -Exist
        }
        It "view Category that SubCategory belongs to" -Tags "ADM-CAT-05","v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" `
                | Select-Object -ExpandProperty Category
                | Select-Object -ExpandProperty Name
                | Should -Be "Bills"
        }
        It "multiple SubCategories have same name" -Tags "ADM-CAT-06","v0.1.1" {
            # Requirement
            New-SubCategory -AccessToken $token -Category "Bills" -Name "Subscription"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            Get-SubCategory -AccessToken $token -Name "Subscription" `
                | Should -HaveCount 2
        }
        It "view SubCategories by Category" -Tags "ADM-CAT-07","v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            # Requirement
            Get-SubCategory -AccessToken $token -Category "Bills" `
                | Should -HaveCount 2
        }
        It "view all SubCategories" -Tags "ADM-CAT-08","v0.1.1" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            # Requirement
            Get-SubCategory -AccessToken $token `
                | Should -HaveCount 3
        }
        It "view remaining Funds" -Tags "ADM-CAT-09","v0.3.0" {
            # Requirement
            Get-Category -AccessToken $token -Name "Bills" `
                | Select-Object -ExpandProperty CurrentFunds `
                | Should -Be 0
        }
        It "view total remaining Funds" -Tags "ADM-CAT-10","v0.3.0" {
            # Requirement
            Get-Category -AccessToken $token `
                | Measure-Object CurrentFunds -Sum `
                | Select-Object -ExpandProperty Sum `
                | Should -Be 0
        }
        It "view remaining Funds from SubCategory" -Tags "ADM-CAT-11","v0.3.1" {
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
        It "create Budget by Category" -Tags "ADM-BUD-01","v0.3.2" {
            # Requirement
            New-Budget -AccessToken $token -Category Bills -Target 500 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
        }
        It "create Budget by SubCategory" -Tags "ADM-BUD-02","v0.3.3" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Budget -AccessToken $token -SubCategory "GasBills" -Target 200 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
        }
        It "create Budget by Transaction name" -Tags "ADM-BUD-03","v0.3.3" {
            # Requirement
            New-Budget -AccessToken $token -TransactionName "GAS COMPANY - Bill" -Target 200 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
            # Requirement - parameter alias
            New-Budget -AccessToken $token -ItemName "ELECTRIC COMPANY - Bill" -Target 200 -Currency CanadianDollar -Period Monthly `
                | Should -Exist
        }
    }

    Context "App Access" -Skip {
        It "invite other Users to share Access with" -Tags "ADM-ACC-01","v0.4.0" {
            # Requirement
        }
        It "identify what Accessing" -Tags "ADM-ACC-02","v0.4.1" {
            # Requirement
        }
        It "switch between Accessing" -Tags "ADM-ACC-03","v0.4.2" {
            # Requirement
        }
    }

    Context "Bank Account Synchronization" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "add Chequing Account from a Bank" -Tags "ADM-BNK-01","vB.1.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyChequingAccount" -Bank "MyBank" -Number 1234561 -Currency CanadianDollar `
                | Should -Exist
        }
        It "add eSavings Account from a Bank" -Tags "ADM-BNK-02","vB.1.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyESavingsAccount" -Bank "MyBank" -Number 1234562 -Currency CanadianDollar `
                | Should -Exist
        }
        It "add Credit Card from a Bank" -Tags "ADM-BNK-03","vB.1.0" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyCreditCard" -Bank "MyBank" -Number 1234563 -Currency CanadianDollar `
                | Should -Exist
        }
        It "lists and filters Bank Accounts" -Tags "ADM-BNK-04","vB.1.0" {
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
        It "view Bank Account details" -Tags "ADM-BNK-05","vB.1.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty Bank
                | Should -Be "MyBank"
        }
        It "view Credit Limit" -Tags "ADM-BNK-06","vB.1.3" {
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
        It "update Bank Account details" -Tags "ADM-BNK-07","vB.1.2" {
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
        It "delete Bank Account" -Tags "ADM-BNK-08","vB.1.0" {
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

        It "view single Balance" -Tags "ADM-BKC-01","vB.2.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Select-Object -ExpandProperty CurrentFunds `
                | Should -BeGreaterThan 0
        }
        It "view total Balance" -Tags "ADM-BKC-02","vB.2.0" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" `
                | Measure-Object CurrentFunds -Sum `
                | Select-Object -ExpandProperty Sum
                | Should -BeGreaterThan 0
        }
        It "view Transactions" -Tags "ADM-BKC-03","vB.1.0" {
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
