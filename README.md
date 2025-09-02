# Repo Starter

## Description

Repo Starter is a binary PowerShell module used for creating elements commonly found in Git repositories.

## Features

At the moment, all Repo Starter does is create a `README.md`, an empty `.gitignore`, and a couple empty folders for holding source code and tests, but further additions are planned. (See "Development Roadmap" for more.)

For creating a Git repository along with all other items that Repo Starter provides functionality to create, use the `NewGitRepository.psm1` file found in `Scripts`. Just be sure to also have a Repo Starter module as well, as that `.psm1` relies on functionality provided by one. Git will also be required.

Repo Starter is compatible with PowerShell 5.1 and targets .NET Framework 4.8.1.

## Usage
### `New-ReadMe` cmdlet

The `New-ReadMe` cmdlet will generate a `README.md` file with a title, different sections for items common to `README`s found in repositories (`Usage`, `License`, `Contributers`, etc.). It can even be given an initial logo by providing an argument with a relative or absolute path (`-AbsoluteLogoPath` and `-RelativeLogoPath` respectively) to the image.

```powershell
New-ReadMe "My Project" -Directory ".\" -RelativeLogoPath "images/logo.myproject.png" -LogoText "My fancy project logo!"
```

### `NewGitRepository.psm1`

If both the `RepoStarter.dll` and `NewGitRepository.psm1` modules are installed, the latter can be called in PowerShell to create a Git repository, a `README`, an empty `.gitignore`, and a pair of folders for holding source code and tests. A `-Project` value is mandatory, and an optional `-DefaultBranch` value can be provided as well for naming the repository's default branch.

```powershell
New-GitRepository -Project "Cool Logger" -DefaultBranch "master"
```

## Installation

These instructions detail how to compile Repo Starter from source.

This project was originally made using Visual Studio 2022 (Community Edition in my case), so a compatible version of said IDE will be required.

The `RepoStarter` project should target .NET Framework 4.8.1 and use C# 12.0. In fact, those settings are already present in the `.csproj` file.

1. Create a new Git repository on your machine and `git clone` this one.
2. Open up the Visual Studio solution.
4. Install the `Microsoft.PowerShell.5.1.ReferenceAssemblies` NuGet package via Visual Studio's NuGet UI or by running the command below:

```
dotnet add package Microsoft.PowerShell.5.1.ReferenceAssemblies
```

4. Build the project via the Visual Studio UI or by running the `dotnet build` command.
5. Copy and paste the DLL found at the end of the `bin/` directory into a path where PowerShell can find it.

For instructions on installing custom PowerShell modules, check out the official documentation at https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_modules?view=powershell-7.5.

For using the `NewGitRepository.psm1` module, simply copy and paste it into its own directory where PowerShell can find it as well.

## Development Roadmap

- [ ] Generation of CHANGELOG files.
- [ ] Generation of LICENSE files.
- [ ] `NewGitRepository.psm1` generates CHANGELOG and LICENSE files in addition to what it already provides.
