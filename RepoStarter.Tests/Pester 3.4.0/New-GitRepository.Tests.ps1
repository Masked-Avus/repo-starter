Describe "New-GitRepository" {
    Context "When command is called" {
        $outputDirectory = ".\Output\New-GitRepository.Tests"

        if (Test-Path $outputDirectory) {
            Remove-Item -Path $outputDirectory -Recurse -Force
        }

        New-Item -Path $outputDirectory -ItemType "Directory"

        New-GitRepository -ProjectName "Testing New-GitRepository" -Directory $outputDirectory -DefaultBranch "main"

        # Since .git files are hidden when created, you will have to check for its existence manually.

        $sourceFolderPath = [System.String]::Concat($outputDirectory, "\src\")

        It "creates a source folder in output path" {
            Test-Path $sourceFolderPath | Should Be $true
        }

        $testsFolderPath = [System.String]::Concat($outputDirectory, "\tests\")

        It "creates a tests folder in output path" {
            Test-Path $testsFolderPath | Should Be $true
        }

        $readMePath = [System.String]::Concat($outputDirectory, "\README.md")

        # For some unknown reason, whenever I run this test, the README does not get generated,
        #     so this test inevitably fails.
        # But when I use New-GitRepository in regular contexts (i.e., calling it from PowerShell
        #     and not from this test file), it generates everything completely fine.
        It "creates a README in output path" {
            Test-Path $readMePath | Should Be $true
        }

        $changeLogPath = [System.String]::Concat($outputDirectory, "\CHANGELOG.md")

        It "creates a CHANGELOG in output path" {
            Test-Path $changeLogPath | Should Be $true
        }

        $gitIgnorePath = [System.String]::Concat($outputDirectory, "\.gitignore")

        It "creates a .gitignore in output path" {
            Test-Path $gitIgnorePath | Should Be $true
        }
    }
}