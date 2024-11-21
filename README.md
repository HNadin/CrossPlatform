# CrossPlatform Labs
Chaban Nadiia IPZ-33. Variant - 2

This repository contains three labs: **Lab1-Lab7 and Lab13**. Below you will find instructions on how to build, run, and test these projects.

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
# For Lab4 in ubuntu
NChaban run lab1 --input /home/vagrant/project/Lab4/Lab1/INPUT.txt --output /home/vagrant/project/Lab4/Lab1/OUTPUT.txt
NChaban run lab2 --input /home/vagrant/project/Lab4/Lab2/INPUT.txt --output /home/vagrant/project/Lab4/Lab2/OUTPUT.txt
NChaban run lab3 --input /home/vagrant/project/Lab4/Lab3/INPUT.txt --output /home/vagrant/project/Lab4/Lab3/OUTPUT.txt
# For Lab4 in windows
NChaban run lab1 --input ../Lab1/INPUT.txt --output ../Lab1/OUTPUT.txt
NChaban run lab2 --input ../Lab2/INPUT.txt --output ../Lab2/OUTPUT.txt
NChaban run lab3 --input ../Lab3/INPUT.txt --output ../Lab3/OUTPUT.txt
dotnet run --urls "https://0.0.0.0:7128" # For Lab5 in ubuntu
```
## Testing

``` bash
dotnet build Build.proj -p:Solution=Lab1 -t:Test   # For Lab1
dotnet build Build.proj -p:Solution=Lab2 -t:Test   # For Lab2
dotnet build Build.proj -p:Solution=Lab3 -t:Test   # For Lab3
```
