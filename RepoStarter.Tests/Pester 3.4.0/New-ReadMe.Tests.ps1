Describe "New-ReadMe" {
    Context "When command is called" {
        $outputDirectory = ".\Output\New-Readme.Tests"

        if (-not (Test-Path $outputDirectory)) {
            New-Item -Path $outputDirectory -ItemType "Directory"
        }

        $outputPath = [System.String]::Concat($outputDirectory, "\README.md")
        
        if (Test-Path $outputPath) {
            Remove-Item -Path $outputPath
        }

        $projectName = "Testing New-ReadMe"
        $logoPath = ".\Images\TestLogo.png"
        $logoText = "Test Logo"

        New-ReadMe -ProjectName $projectName -Directory $outputDirectory -LogoPath $logoPath -LogoText $logoText

        It "creates README in output path" {
            Test-Path $outputPath | Should Be $true
        }
    }
}