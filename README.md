# CrossPlatform Labs
Chaban Nadiia IPZ-33. Variant - 2

This repository contains three labs: **Lab1**, **Lab2**, and **Lab3**, each of which includes both the main project and unit tests. Below you will find instructions on how to build, run, and test these projects.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Running Projects](#running-projects)
- [Testing](#testing)
- [Running All Projects and Tests](#running-all-projects-and-tests)

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/download) version 6.0 or higher
- MSBuild (included with Visual Studio or installable separately)

You can verify if .NET SDK is installed by running:

```bash
dotnet --version
```

## Running Projects

You can run the projects in two ways:

### Running Projects Individually

If you want to run a specific project, navigate to the project directory in your terminal and execute one of the following commands:

``` bash
dotnet build Lab1/Lab1/Lab1.csproj
dotnet run --project Lab1/Lab1/Lab1.csproj   # For Lab1
dotnet build Lab2/Lab2/Lab2.csproj
dotnet run --project Lab2/Lab2/Lab2.csproj   # For Lab2
dotnet build Lab3/Lab3/Lab3.csproj
dotnet run --project Lab3/Lab3/Lab3.csproj   # For Lab3
```
## Testing

``` bash
dotnet test Lab1/Lab1.Tests/Lab1.Tests.csproj   # For Lab1
dotnet test Lab2/Lab2.Tests/Lab2.Tests.csproj   # For Lab2
dotnet test Lab3/Lab3.Tests/Lab3.Tests.csproj   # For Lab3
```

## Running All Projects and Tests

If you want to build and run all projects sequentially, you can use MSBuild. To do this, execute the following command:

```bash
dotnet msbuild /t:BuildAndRunProjects
```
