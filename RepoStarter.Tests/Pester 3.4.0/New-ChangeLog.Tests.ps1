Describe "New-ChangeLog" {
    Context "When command is called" {
        $outputDirectory = ".\Output\New-ChangeLog.Tests"

        if (-not (Test-Path $outputDirectory)) {
            New-Item -Path $outputDirectory -ItemType "Directory"
        }

        $outputPath = ".\Output\New-ChangeLog.Tests\CHANGELOG.md"

        if (Test-Path $outputPath) {
            Remove-Item -Path $outputPath
        }

        New-ChangeLog -Directory ".\Output\New-ChangeLog.Tests" -Versions "v.0.2.0","v0.1.0"
        
        It "creates CHANGELOG in output path" {
            Test-Path $outputPath | Should Be $true
        }
    }
}