Get-ChildItem $PSScriptRoot\UseCases -PipelineVariable file `
    | Get-Content `
    | Select-String 'It "' `
    | ForEach-Object {
        if ($_ -match 'It "([-,\w\s]+)".*"(v[\w\d]+.[\d]+.[\d]+)"') {
            [PSCustomObject]@{Requirement=$Matches[1];Version=$Matches[2];File=$file.Name;Line=$_}
        } else {
            [PSCustomObject]@{Requirement='';Version='No Version';File=$file.Name;Line=$_}
        }
    } `
    | Sort-Object Version
