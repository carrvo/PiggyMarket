Describe "Active Influence on and Decisions against the system (ongoing Modification)" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Context "Categories" {
        It "view Category" -Tags "ADM-CAT-01","v0.1.1","Simple" {
            # Requirement
            Get-Category -AccessToken $token -Name "Bills" |
                Should -Exist
        }
        It "view all Categories" -Tags "ADM-CAT-02","v0.1.1","Simple" {
            # Requirement
            Get-Category -AccessToken $token |
                Should -BeLessOrEqual 15

            # Rationale
            <#
            Must be of the specified number. The importance here
            is that categories must be limited so that they can
            retain their purpose of aggregating and summarizing
            transactions, providing a course-grained view. If they
            are let to be more, there is risk of overwhelming the
            summary with too much information and no longer
            allowing it to remain high-level.
            #>
        }
        It "create SubCategory" -Tags "ADM-CAT-03","v0.1.1","Simple" {
            # Requirement
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills" |
                Should -Exist
        }
        It "view SubCategory" -Tags "ADM-CAT-04","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" |
                Should -Exist
        }
        It "view Category that SubCategory belongs to" -Tags "ADM-CAT-05","v0.1.1","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" |
                Select-Object -ExpandProperty Category |
                Select-Object -ExpandProperty Name |
                Should -Be "Bills"
        }
        It "multiple SubCategories have same name" -Tags "ADM-CAT-06","v0.1.1","Moderate" {
            # Requirement
            New-SubCategory -AccessToken $token -Category "Bills" -Name "Subscription"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            Get-SubCategory -AccessToken $token -Name "Subscription" |
                Should -HaveCount 2
        }
        It "view SubCategories by Category" -Tags "ADM-CAT-07","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            # Requirement
            Get-SubCategory -AccessToken $token -Category "Bills" |
                Should -HaveCount 2
        }
        It "view all SubCategories" -Tags "ADM-CAT-08","v0.1.1","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"
            New-SubCategory -AccessToken $token -Category "Bills" -Name "ElectricBills"
            New-SubCategory -AccessToken $token -Category "Entertainment" -Name "Subscription"

            # Requirement
            Get-SubCategory -AccessToken $token |
                Should -HaveCount 3
        }
        It "view remaining Funds" -Tags "ADM-CAT-09","v0.3.0","Moderate" {
            # Requirement
            Get-Category -AccessToken $token -Name "Bills" |
                Select-Object -ExpandProperty CurrentFunds |
                Should -Be 0
        }
        It "view total remaining Funds" -Tags "ADM-CAT-10","v0.3.0","Complex" {
            # Requirement
            Get-Category -AccessToken $token |
                Measure-Object CurrentFunds -Sum |
                Select-Object -ExpandProperty Sum |
                Should -Be 0
        }
        It "view remaining Funds from SubCategory" -Tags "ADM-CAT-11","v0.3.1","Complex" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" |
                Select-Object -ExpandProperty Category |
                Select-Object -ExpandProperty CurrentFunds |
                Should -Be 0
        }
        It "deletes SubCategory" -Tags "ADM-CAT-12","v0.3.4","Simple" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            Get-SubCategory -AccessToken $token -Name "GasBills" |
                Remove-SubCategory
            Get-SubCategory -AccessToken $token |
                Select-Object -ExpandProperty Name |
                Should -Not -Contain "GasBills"
        }
    }

    Context "Budgeting" {
        It "create Budget by Category" -Tags "MAN-BUD-01","v0.3.2","Simple" {
            # Requirement
            New-Budget -AccessToken $token -Category "Bills" -Target 500 -Currency CanadianDollar -Period Monthly |
                Should -Exist
        }
        It "create Budget by SubCategory" -Tags "MAN-BUD-02","v0.3.3","Moderate" {
            # Pre-Requisite
            New-SubCategory -AccessToken $token -Category "Bills" -Name "GasBills"

            # Requirement
            New-Budget -AccessToken $token -SubCategory "GasBills" -Target 200 -Currency CanadianDollar -Period Monthly |
                Should -Exist
        }
        It "create Budget by Transaction name" -Tags "MAN-BUD-03","v0.3.3","Complex" {
            # Requirement
            New-Budget -AccessToken $token -TransactionName "GAS COMPANY - Bill" -Target 200 -Currency CanadianDollar -Period Monthly |
                Should -Exist
            # Requirement - parameter alias
            New-Budget -AccessToken $token -ItemName "ELECTRIC COMPANY - Bill" -Target 200 -Currency CanadianDollar -Period Monthly |
                Should -Exist
        }
        It "modifies Budget" -Tags "MAN-BUD-04","v0.3.4","Complex" {
            # Pre-Requisite
            New-Budget -AccessToken $token -Category "Bills" -Target 500 -Currency CanadianDollar -Period Monthly

            # Requirement
            Get-Budget -AccessToken $token -Category "Bills" |
                Edit-Budget -Target 125 -Period Weekly |
                Select-Object -ExpandProperty Target |
                Should -Be 125
        }
        It "deletes Budget" -Tags "MAN-BUD-05","v0.3.4","Moderate" {
            # Pre-Requisite
            New-Budget -AccessToken $token -Category "Bills" -Target 500 -Currency CanadianDollar -Period Monthly

            # Requirement
            Get-Budget -AccessToken $token -Category "Bills" |
                Remove-Budget
            Get-Budget -AccessToken $token -Category "Bills" |
                Should -Not -Exist
        }
    }

    Context "Goals" {
        It "create Goal" -Tags "MAN-GOL-01","v1.1.1","Simple" {
            # Requirement
            New-Goal -AccessToken $token -SubCategory "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar |
                Should -Exist
            Get-SubCategory -AccessToken $token -Name "Theatre" |
                Should -Exist
            # Requirement - Parameter Alias
            New-Goal -AccessToken $token -Name "Theatre2" -Category "Entertainment" -Target 120 -Currency CanadianDollar |
                Should -Exist
            Get-SubCategory -AccessToken $token -Name "Theatre2" |
                Should -Exist

            # Rationale
            <#
            Using a SubCategory allows for an easy and intuitive way of tracking transactions
            against the goal. However, it means that the SubCategory should be dedicated to
            the goal and not used for any other means. These Goal specific SubCategories
            are to be transient and only last for the duration of the Goal.
            #>
        }
        It "create accrual Goal" -Tags "MAN-GOL-02","v1.1.2","Moderate" {
            # Requirement
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly |
                Should -Exist
            Get-SubCategory -AccessToken $token -Name "Theatre" |
                Should -Exist
        }
        It "view Goal" -Tags "MAN-GOL-03","v1.1.1","Simple" {
            # Pre-Requisite
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly |

            # Requirement
            Get-Goal -AccessToken $token -SubCategory "Theatre" |
                Select-Object -ExpandProperty Target |
                Should -Be 120
            # Requirement - Parameter Alias
            Get-Goal -AccessToken $token -Name "Theatre" |
                Should -Exist
        }
        It "abandons Goal" -Tags "MAN-GOL-04","v1.1.2","Simple" {
            # Pre-Requisite
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly |

            # Requirement
            Get-Goal -AccessToken $token -Name "Theatre" |
                Deny-Goal
            Get-SubCategory -AccessToken $token -Name "Theatre" |
                Should -Not -Exist
        }
        It "modifies Goal" -Tags "MAN-GOL-05","v1.1.2","Complex" {
            # Pre-Requisite
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target 120 -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly |

            # Requirement
            Get-Goal -AccessToken $token -Name "Theatre" |
                Edit-Goal -Target 240 -PeriodicTarget 5 -Period Weekly
        }
    }
}
