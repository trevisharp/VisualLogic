clear
"Iniciando dotnet build..."
$output = dotnet build
if ($output[$output.Length - 3][4] -ne "0")
{
    "Foram encontrados " + $output[$output.Length - 3][4] + " erros ao executar dotnet build."
    return
}
"dotnet build concluido com sucesso!"
"Iniciando dotnet pack..."
$output = dotnet pack -c Release
if ($output[$output.Length - 1].Substring(2, 17) -ne "Pacote criado com")
{
    "Pacote ja criado ou com erros na execucao do dotnet pack -c Release."
    return
}
"dotnet pack concluido com sucesso"
"Publicando pacote em nuget.org..."
$apikey = $null
if ($args.Length -gt 0)
{
    $apikey = $args[0]
    Set-Content "apikey.env" $apikey
}
else
{
    $output = ls
    $i = 0
    while ($i -lt $output.Length)
    {
        if ($output[$i].Name -eq "apikey.env")
        {
            $apikey = Get-Content "apikey.env"
            break
        }
        $i++
    }
}
dotnet nuget push .\bin\Release\VisualLogic.1.0.0.nupkg --api-key $apikey --source https://api.nuget.org/v3/index.json