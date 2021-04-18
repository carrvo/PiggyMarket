Describe "User-Initiated Informing or Viewing" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\Configuration\Application.psd1
        $token = Grant-AccessToken -All
    }
}
