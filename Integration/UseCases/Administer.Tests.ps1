Describe "Administration of Users and Access" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "App Access" -Skip {
        It "invite other Users to share Access with" -Tag "ADM-ACC-01","v0.4.0","Complex" {
            # Requirement
            Grant-AccountContext -Email "partner@someaddress.com"

            # Rationale
            <#
            Couples and families, and other circumstances that cause
            multiple users to have shared finances, may require multiple
            users to budget together under a single context.
            #>
        }
        It "identify what Accessing" -Tag "ADM-ACC-02","v0.4.1","Moderate" {
            # Requirement
            Get-AccountContext -Current |
                Should -Exist

            # Rationale
            <#
            The application should use the Current context in the background
            without the user needing to specify what context they are in.
            #>
        }
        It "creates new application context for Access separation" -Tag "ADM-ACC-04","v0.4.2","Complex" {
            # Requirement
            New-AccountContext -Name "MyOtherContext"
            Get-AccountContext -Name "MyOtherContext" |
                Should -Exist

            # Rationale
            <#
            This provides a mechanism for users to control what transactions
            they share with other users.
            #>
        }
        It "switch between Accessing" -Tag "ADM-ACC-03","v0.4.2","Moderate" {
            # Requirement
            Set-AccountContext -Name "MyOtherContext"
            Get-AccountContext -Current |
                Select-Object -ExpandProperty Name |
                Should -Be "MyOtherContext"
        }
    }

    Context "Bank Account Synchronization" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "add Chequing Account from a Bank" -Tag "ADM-BNK-01","vB.1.0","Simple" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyChequingAccount" -Bank "MyBank" -Number 1234561 -Currency CanadianDollar |
                Should -Exist
        }
        It "add eSavings Account from a Bank" -Tag "ADM-BNK-02","vB.1.0","Simple" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyESavingsAccount" -Bank "MyBank" -Number 1234562 -Currency CanadianDollar |
                Should -Exist
        }
        It "add Credit Card from a Bank" -Tag "ADM-BNK-03","vB.1.0","Simple" {
            # Requirement
            Add-BankAccount -AccessToken $token -Name "MyCreditCard" -Bank "MyBank" -Number 1234563 -Currency CanadianDollar |
                Should -Exist
        }
        It "lists and filters Bank Accounts" -Tag "ADM-BNK-04","vB.1.0","Moderate" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount1" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            Add-BankAccount -AccessToken $token -Name "MyAccount2" -Bank "MyBank" -Number 1234568 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token |
                Should -HaveCount 2
            Get-BankAccount -AccessToken $token |
                Select-Object -ExpandProperty Name |
                Should -Contain "MyAccount2"
            Get-BankAccount -AccessToken $token -Bank "MyBank" |
                Should -HaveCount 2
        }
        It "view Bank Account details" -Tag "ADM-BNK-05","vB.1.0","Simple" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Select-Object -ExpandProperty Bank |
                Should -Be "MyBank"
        }
        It "view Credit Limit" -Tag "ADM-BNK-06","vB.1.3","Moderate" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            Add-BankAccount -AccessToken $token -Name "MyCreditCard" -Bank "MyBank" -Number 1234563 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Select-Object -ExpandProperty CreditLimit |
                Should -Not -Exist
            Get-BankAccount -AccessToken $token -Name "MyCreditCard" |
                Select-Object -ExpandProperty CreditLimit |
                Should -BeGreaterThan 0
        }
        It "update Bank Account details" -Tag "ADM-BNK-07","vB.1.2","Moderate" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Update-BankAccount -BankName "MyAlternateBank" -Number 1234557
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Update-BankAccount -Currency CanadianDollar
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Update-BankAccount -Name "MyNewName"

            Get-BankAccount -AccessToken $token -Name "MyNewName" |
                Select-Object -ExpandProperty Bank |
                Should -Be "MyAlternateBank"
        }
        It "delete Bank Account" -Tag "ADM-BNK-08","vB.1.0","Simple" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Remove-BankAccount
        }
    }

    Context "View Account Content" -Skip {
        BeforeAll {
            . $PSScriptRoot\BankMoq.ps1
        }

        It "view single Balance" -Tag "ADM-BKC-01","vB.2.0","Simple" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Select-Object -ExpandProperty CurrentFunds |
                Should -BeGreaterThan 0
        }
        It "view total Balance" -Tag "ADM-BKC-02","vB.2.0","Moderate" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar

            # Requirement
            Get-BankAccount -AccessToken $token -Name "MyAccount" |
                Measure-Object CurrentFunds -Sum |
                Select-Object -ExpandProperty Sum |
                Should -BeGreaterThan 0
        }
        It "view Transactions" -Tag "ADM-BKC-03","vB.1.0","Simple" {
            # Pre-Requisite
            Add-BankAccount -AccessToken $token -Name "MyAccount" -Bank "MyBank" -Number 1234567 -Currency CanadianDollar
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-Transaction -AccessToken $token -Name "GAS COMPANY - Bill" -Payee "GasCompany" -SubCategory "GasBills" -BankAccount "MyAccount" -Price -100 -Currency CanadianDollar -ReceiptID Gas

            # Requirement
            Get-Transactions -BankAccount "MyAccount" |
                Should -HaveCount 1
        }
    }
}
