Describe "Automatically Informs Users" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Describe "Notifications" {
        It "Sends Overbudget Notification" -Tags "RPT-NFY-01","v0.5.0","Moderate" {
            $target = 20
            $budget = New-Budget -SubCategory CustomEntertainment2 -Target $target -Currency CanadianDollar -Period Monthly

            $trans = $target + 5
            New-Transaction -Payee Ent02 -SubCategory CustomEntertainment2 -BankAccount MyAccount -Price -$trans -Currency CanadianDollar -ReceiptID "Ent02-$trans"
            Get-Notification | Where-Object Reason -EQ "Over Budget" | Where-Object Category -EQ Entertainment | Should -Not -BeNullOrEmpty
        }
        It "Sends Budget Met Notification" -Tags "RPT-NFY-02","v0.5.0","Moderate" {
            $target = 20
            $budget = New-Budget -SubCategory CustomBills2 -Target $target -Currency CanadianDollar -Period Monthly

            $trans = $target
            New-Transaction -Payee Bill02 -SubCategory CustomBills2 -BankAccount MyAccount -Price -$trans -Currency CanadianDollar -ReceiptID "Bill02-$trans"
            Get-Notification | Where-Object Reason -EQ "At Budget" | Where-Object Category -EQ Bills | Should -Not -BeNullOrEmpty
        }
        It "Sends Bounced Notification" -Tags "RPT-NFY-03","vB.3.0","Complex" {
            Add-BankAccount -Name "PoorAccount" -BankName "MyBank" -Number 1234569 -Currency CanadianDollar
            New-Transaction -Payee Bill02 -SubCategory CustomBills2 -BankAccount PoorAccount -Price -10 -Currency CanadianDollar -ReceiptID "Bill02-Poor"
            Get-Notification | Where-Object Reason -EQ Bounced | Should -Not -BeNullOrEmpty
        }
        It "Sends Below Threshold Notification" -Tags "RPT-NFY-04","v0.5.1","Complex" {
            Add-BankAccount -Name "NotificationAccount" -BankName "MyBank" -Number 1234560 -Currency CanadianDollar
            Limit-BankAccount -Name "NotificationAccount" -Above 6
            $target = Get-BankAccount NotificationAccount | ForEach-Object CurrentFunds
            $budget = New-Budget -SubCategory "CustomPersonal Care2" -Target $target -Currency CanadianDollar -Period Monthly

            $trans = $target - 5
            New-Transaction -Payee Care2 -SubCategory "CustomPersonal Care2" -BankAccount NotificationAccount -Price -$trans -Currency CanadianDollar -ReceiptID "Care02-$trans"
            Get-Notification | Where-Object Reason -EQ "Below Threshold" | Where-Object Category -EQ "Personal Care" | Should -Not -BeNullOrEmpty
            Get-Notification | Where-Object Reason -EQ "Below Threshold" | Where-Object Account -EQ "NotificationAccount" | Should -Not -BeNullOrEmpty
        }
        It "Sends Above Threshold Notification" -Tags "RPT-NFY-05","v0.5.1","Complex" {
            Add-BankAccount -Name "NotificationAccount" -BankName "MyBank" -Number 1234560 -Currency CanadianDollar
            $target = Get-BankAccount NotificationAccount | ForEach-Object CurrentFunds
            Limit-BankAccount -Name "NotificationAccount" -Below ($target-1)

            $trans = $target
            New-Transaction -Payee Care2 -SubCategory "CustomPersonal Care3" -BankAccount NotificationAccount -Price $trans -Currency CanadianDollar -ReceiptID "Care03+$trans"
            Get-Notification | Where-Object Reason -EQ "Above Threshold" | Where-Object Account -EQ "NotificationAccount" | Should -Not -BeNullOrEmpty
        }
		It "Sends Goal Met Notification" -Tags "RPT-GOL-01" {
            # Pre-Requisite
			$target = 120
            New-Goal -AccessToken $token -Name "Theatre" -Category "Entertainment" -Target $target -Currency CanadianDollar -PeriodicTarget 10 -Period Monthly |
            $trans = $target
            New-Transaction -Payee Care2 -SubCategory "Theatre" -Price $trans -Currency CanadianDollar -ReceiptID "Goal+$trans"

			# Requirement
			Get-Goal -AccessToken $token -Name "Theatre" |
				Confirm-Goal
			Get-Notification | Where-Object Reason -EQ "Goal Met" | Should -Not -BeNullOrEmpty
		}
    }
}
