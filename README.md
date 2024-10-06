# CrossPlatform Labs
Chaban Nadiia IPZ-33. Variant - 2

This repository contains three labs: **Lab1**, **Lab2**, and **Lab3**, each of which includes both the main project and unit tests. Below you will find instructions on how to build, run, and test these projects.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Running Projects](#running-projects)
- [Testing](#testing)

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
dotnet build Build.proj -p:Solution=Lab1 -t:Build 
dotnet build Build.proj -p:Solution=Lab1 -t:Run   # For Lab1
dotnet build Build.proj -p:Solution=Lab2 -t:Build 
dotnet build Build.proj -p:Solution=Lab2 -t:Run   # For Lab2
dotnet build Build.proj -p:Solution=Lab3 -t:Build 
dotnet build Build.proj -p:Solution=Lab3 -t:Run   # For Lab3
```
## Testing

``` bash
dotnet build Build.proj -p:Solution=Lab1 -t:Test   # For Lab1
dotnet build Build.proj -p:Solution=Lab2 -t:Test   # For Lab2
dotnet build Build.proj -p:Solution=Lab3 -t:Test   # For Lab3
```
