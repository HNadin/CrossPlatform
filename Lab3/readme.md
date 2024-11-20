
``` bash
cd Lab3

#Starting the build process in Release mode...
dotnet build -c Release

#Creating a NuGet package from the Lab3 project...
dotnet pack Lab3.csproj -c Release -o ./nupkg

cd ..
cd Lab3.Runner

#Installing the NuGet package into the Lab3.Runner project...
dotnet add package NChaban_ -version 1.0.3 --source ../Lab3/nupkg

#Restoring all project dependencies...
dotnet restore

#Executing the Lab3.Runner project...
dotnet run --project Lab3.Runner/Lab3.Runner.csproj
```
