Describe "Automatically Informs Users" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }

    Describe "Notifications" {
        It "Sends Overbudget Notification" -Tags "v0.5.0" {
            $target = 20
            $budget = Add-Budget -SubCategory CustomEntertainment2 -Target $target -Currency CanadianDollar -Period Monthly

            $trans = $target + 5
            Add-Transaction -Payee Ent02 -SubCategory CustomEntertainment2 -BankAccount MyAccount -Price -$trans -Currency CanadianDollar -ReceiptID "Ent02-$trans"
            List-Notifications | Where Reason -EQ "Over Budget" | Where Category -EQ Entertainment | Should -Not -BeNullOrEmpty
        }
        It "Sends Budget Met Notification" -Tags "v0.5.0" {
            $target = 20
            $budget = Add-Budget -SubCategory CustomBills2 -Target $target -Currency CanadianDollar -Period Monthly

            $trans = $target
            Add-Transaction -Payee Bill02 -SubCategory CustomBills2 -BankAccount MyAccount -Price -$trans -Currency CanadianDollar -ReceiptID "Bill02-$trans"
            List-Notifications | Where Reason -EQ "At Budget" | Where Category -EQ Bills | Should -Not -BeNullOrEmpty
        }
        It "Sends Bounced Notification" -Tags "vB.3.0" {
            Add-BankAccount -Name "PoorAccount" -BankName "MyBank" -Number 1234569 -Currency CanadianDollar
            Add-Transaction -Payee Bill02 -SubCategory CustomBills2 -BankAccount PoorAccount -Price -10 -Currency CanadianDollar -ReceiptID "Bill02-Poor"
            List-Notifications | Where Reason -EQ Bounced | Should -Not -BeNullOrEmpty
        }
        It "Sends Below Threshold Notification" -Tags "v0.5.1" {
            Add-BankAccount -Name "NotificationAccount" -BankName "MyBank" -Number 1234560 -Currency CanadianDollar
            Limit-BankAccount -Name "NotificationAccount" -Above 6
            $target = Get-BankAccount NotificationAccount | ForEach-Object CurrentFunds
            $budget = Add-Budget -SubCategory "CustomPersonal Care2" -Target $target -Currency CanadianDollar -Period Monthly

            $trans = $target - 5
            Add-Transaction -Payee Care2 -SubCategory "CustomPersonal Care2" -BankAccount NotificationAccount -Price -$trans -Currency CanadianDollar -ReceiptID "Care02-$trans"
            List-Notifications | Where Reason -EQ "Below Threshold" | Where Category -EQ "Personal Care" | Should -Not -BeNullOrEmpty
            List-Notifications | Where Reason -EQ "Below Threshold" | Where Account -EQ "NotificationAccount" | Should -Not -BeNullOrEmpty
        }
        It "Sends Above Threshold Notification" -Tags "v0.5.1" {
            Add-BankAccount -Name "NotificationAccount" -BankName "MyBank" -Number 1234560 -Currency CanadianDollar
            $target = Get-BankAccount NotificationAccount | ForEach-Object CurrentFunds
            Limit-BankAccount -Name "NotificationAccount" -Below ($target-1)

            $trans = $target
            Add-Transaction -Payee Care2 -SubCategory "CustomPersonal Care3" -BankAccount NotificationAccount -Price $trans -Currency CanadianDollar -ReceiptID "Care03+$trans"
            List-Notifications | Where Reason -EQ "Above Threshold" | Where Account -EQ "NotificationAccount" | Should -Not -BeNullOrEmpty
        }
    }
}
