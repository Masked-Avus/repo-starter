Describe "New-ReadMe" {
    Context "When command is called" {
        $outputDirectory = ".\Output\New-Readme.Tests"

        if (-not (Test-Path $outputDirectory)) {
            New-Item -Path $outputDirectory -ItemType "Directory"
        }

        $outputPath = ".\Output\New-ReadMe.Tests\README.md"
        
        if (Test-Path $outputPath) {
            Remove-Item -Path $outputPath
        }

        $projectName = "Testing New-ReadMe"
        $outputDirectory = ".\Output\New-ReadMe.Tests"

        New-ReadMe -ProjectName $projectName -Directory $outputDirectory

        It "creates README in output path" {
            Test-Path $outputPath | Should Be $true
        }
    }
}