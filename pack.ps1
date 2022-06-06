dotnet pack
dotnet nuget push .\bin\Debug\VisualLogic.1.0.0.nupkg --api-key $args[0] --source https://api.nuget.org/v3/index.json