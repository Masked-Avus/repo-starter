# Repo Starter

## Description

Repo Starter is a PowerShell binary module used for creating elements commonly found in Git repositories.

## Features

At the moment, Repo Starter itself creates the following (See "Development Roadmap" for more on future additions.):
- `README.md`
- `CHANGELOG.md`
- `LICENSE.md`
- Empty `.gitignore`
- `src` folder
- `tests` folder

Repo Starter was made with PowerShell 5.1 in mind and targets .NET Framework 4.8.1.

## Usage
### `New-ReadMe` cmdlet

The `New-ReadMe` cmdlet will generate a `README.md` file with a title, different sections for items common to `README`s found in repositories (`Usage`, `License`, `Contributers`, etc.). It requires a `-ProjectName` value for the title, and it can take the following optional parameters:
- `-Directory`: Sets the location where the `README` is to be generated. Defaults to the current directory if not specified.
- `-LogoPath`: Path of an image to be referenced at the top of the resulting file. Used for providing a logo.
- `-LogoText`: Underlying text used within the Markdown link related to the logo. Ignored if `-LogoPath` is not provided.

```powershell
New-ReadMe "Cool Logger" -Directory ".\" -Path "images/CoolLogger.png" -LogoText "My fancy project logo!"
```

### `New-ChangeLog` cmdlet

The `New-ChangeLog` cmdlet will generate an empty `CHANGELOG.md` file by default. It can take the following optional parameters:
- `-Directory`: Sets the location where the `CHANGELOG` is to be generated. Defaults to the current directory if not specified.
- `-Versions`: An array of strings representing version names that will be listed in the resulting file. These version names must be provided in *descending* order to ensure that later versions will appear first in the resulting file.
- `-Title`: Custom title for the initial heading.

```powershell
New-ChangeLog -Directory ".\" -Versions "v0.2.0", "v0.1.0" -Title "Cool Logger's History"
```

### `New-License` cmdlet

The `New-License` cmdlet will generate a `LICENSE.md` file. It requires a `-LicenseType` argument that is given the case-insensitive name of a supported type of open-source license. As of right now, The following open-source licenses are supported: `"Apache"`, `"BSD3"`, `"GPL3"`, `"MIT"`, `"MPL"`, `"PublicDomain"`/`"Public"`, and `"zlib"`. The cmdlet can take the following additional arguments:
- `-Directory`: Sets the location where the `LICENSE` is to be generated. Defaults to the current directory if not specified.
- `-ProjectName`: Name of the project to be mentioned in licenses like BSD3 and GPL3.
- `-Organization`: Name of the owner (individual or group) of the repository.
- `-Year`: The year associated with the given project. Defaults to the current year if not specified.

*Certain licenses require `-ProjectName` and/or `-Organization` values.* For example, an MIT license requires the former while GPL3 requires both. Values not used by certain licenses are ignored.

```powershell
New-License -LicenseType "gpl3" -Directory ".\" -ProjectName "Cool Logger" -Organization "John Doe" -Year 2025
```

### `New-GitRepository` cmdlet

The `New-GitRepository` cmdlet will generate a new Git repository, along with a couple starter folders, a `.gitignore, `a `README`, and a `CHANGELOG`. It requires a `-ProjectName` argument for the `README`, and it can take the following optional commands as well:
- `-Directory`: Sets the location where all items will be created. Defaults to the current directory if not specified.
- `-DefaultBranch`: Sets name of the repository's default branch. Defaults to `master` if not specified.

```powershell
New-GitRepository -ProjectName "Cool Logger" -DefaultBranch "master" -Directory "C:\Users\John\Dev\cool-logger"
```

## Dependencies

The following NuGet packages are external dependencies of Repo Starter:
- `Microsoft.PowerShell.5.1.ReferenceAssemblies` (no need to include this dependency in published projects)

## Installation

These instructions detail how to compile Repo Starter from source.

This project was originally made using Visual Studio 2022 (Community Edition in my case), so a compatible version of said IDE will be required.

The `RepoStarter` project should target .NET Framework 4.8.1 and use C# 12.0. In fact, those settings are already present in the `.csproj` file.

1. Create a new Git repository on your machine and `git clone` this one.
2. Open up the Visual Studio solution.
4. Install the required NuGet packages (see "Dependencies" for which ones) via Visual Studio's NuGet UI or by running the `dotnet add package` command.
4. Build the project via the Visual Studio UI or by running either the `dotnet build` or `dotnet publish` commands.
5. Copy and paste the DLL found at the end of the `RepoStarter\bin\` directory into a path where PowerShell can find it.

For instructions on installing custom PowerShell modules, check out the official documentation [here](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_modules?view=powershell-7.5).

## Development Roadmap

- [X] Generation of `README` files.
- [X] Generation of `CHANGELOG` files.
- [X] Generation of `LICENSE` files.
- [ ] Cmdlet that creates a suite of items for a basic GitHub repository. (Partially met)
