function New-GitRepository() {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Project,
        [PSDefaultValue(Help="Name of Git repository's default branch")]
        [string]$DefaultBranch = "master"
    )

    git init --initial-branch $DefaultBranch
    New-ReadMe -ProjectName $Project -Directory ".\"
    New-ChangeLog -Directory ".\"
    New-Item -Path . -Name "src" -ItemType "Directory"
    New-Item -Path . -Name "tests" -ItemType "Directory"
    New-Item -Path . -Name ".gitignore" -ItemType "File"
}

Export-ModuleMember -Function New-GitRepository
