Describe "License" {
    Context "When command is called" {
        $outputDirectory = ".\Output\New-License.Tests"

        if (-not (Test-Path $outputDirectory)) {
            New-Item -Path $outputDirectory -ItemType "Directory"
        }

        $outputPath = [System.String]::Concat($outputDirectory, "\LICENSE.md")
        
        if (Test-Path $outputPath) {
            Remove-Item -Path $outputPath
        }

        New-License -LicenseType GPL3 -ProjectName "Testing New-License" -Organization "RepoStarter.Tests" -Year 2025 -Directory $outputDirectory

        It "creates LICENSE in output path" {
            Test-Path $outputPath | Should Be $true
        }
    }
}