# VisualLogic
A .NET framework to create educational applications and realize algorithms concepts proof.

[![](https://img.shields.io/badge/Visual-Logic-purple?style=for-the-badge)](https://github.com/trevisharp/VisualLogic)
[![](https://img.shields.io/nuget/dt/VisualLogic?color=purple&style=for-the-badge)](https://www.nuget.org/packages/VisualLogic/)
[![](https://img.shields.io/github/license/Trevisharp/VisualLogic?color=purple&style=for-the-badge)](LICENSE)
[![](https://img.shields.io/github/last-commit/Trevisharp/VisualLogic?color=purple&style=for-the-badge)](https://github.com/trevisharp/VisualLogic/commits/main)
[![](https://img.shields.io/github/commit-activity/m/Trevisharp/VisualLogic?color=purple&style=for-the-badge)](https://github.com/trevisharp/VisualLogic/commits/main)

## Tutorial

O framework possibilita a visualização de estrutura de dados sendo manipuladas.
Isso pode ser usado tanto para fins educacionais, como apresentar desafios de programação como também prova de conceito de algoritmos. Para implementar é simples, basta [instalar a biblioteca](https://www.nuget.org/packages/VisualLogic/) e implementar poucas classes.

A principal é uma classe que herda de LogicApp, aqui você deve definir 3 métodos principais:
- DefineDependencyInjection
- LoadFromParams
- SetRunHooks
Abaixo teremos um exemplo e um guia geral de implementação.

``` cs
using VisualLogic;
using VisualLogic.Elements;

public class SortLogicApp : LogicApp
{
    public int ArrayLength { get; set; }
    protected override void DefineDependencyInjection(DIBuilder builder)
    {
        builder
            .AddMethod("solution")
            .AddInstance(new vRandomArray(100, 1100, ArrayLength));
    }

    protected override void LoadFromParams(AppArgs args)
    {
        this.ArrayLength = args.get(0, 50);
    }

    protected override void SetRunHooks()
    {
        AddRunHook("solution", HookType.OnAppStart);
    }
}
```

Acima é criado uma aplicação para desafiar um possível usuário a ordenar uma lista.
Em LoadFromParams é possível decidir parâmetros de teste para o usuário. Neste caso o usuário poderá definir o tamanho do array a ser ordenado.
Usando o DefineDependencyInjection você define as funções a serem buscadas usando Reflection. No caso uma função solution definida no Top-Level file será armazenada para uso futuro. Usando o AddMethod é possível definir diversas funções a serem chamadas em diversos momentos. Com o AddInstance é possível definir os objetos a serem entregues ao usuário em parâmetros de funções definidas com o AddMethod.
O momento de chamada de cada função é definido pelo SetRunHooks.

``` cs
using VisualLogic;
using VisualLogic.Elements;

LogicApp.Run(25, 40);

void solution(vRandomArray array)
{
    bool ordenado = false;
    while (!ordenado)
    {
        ordenado = true;
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] < array[i + 1])
            {
                var temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
                ordenado = false;
            }
        }
    }
}
```

Ainda sim, é possível não implementar a AppLogic usando valores padrões de cada tipo.
A implementação acima funciona perfeitamente sem o SortLogicApp no projeto. Note que o parâmetro 40, no LogicApp.Run não terá efeito pois a instância padrão de vArray será utilizada.

Normalmente seu .csproj deve ser como o abaixo, uma aplicação UseWindowsForms=true para net6.0-windows.

``` cs
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <UseWindowsForms>true</UseWindowsForms>
    <TargetFramework>net6.0-windows</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="VisualLogic" Version="2.0.0" />
  </ItemGroup>

</Project>
```

Você ainda pode criar seus próprios elementos ou derivar de elementos visuais existentes a partir da classe
VisualElement.

## Visual Elements

### vArray

A simple array initializate with zeros. The default instance is for 50 values between 0 and 1000.

### vRandomArray

Inheriths from vArray and add Random data in the vector. The default instance is for 50 values between 0 and 1000.

### vSurface

A 3D Surface from y = f(x, z) function. The default instance is for f(x, z) = 0 with 0 <= x <= 20.0, 0 <= z <= 5.0, 0 <= y <= 5.0 and resolution of 0.1.

### vRandomSurface

Inheriths from vSurface and add Random data in the function. The default instance is for f(x, z) = 0 with 0 <= x <= 20.0, 0 <= z <= 5.0, 0 <= y <= 5.0 and resolution of 0.1.

## Future Improvements

- Change Tutorial to English
- Create a Full documentation
- Add more default Elements
- Change desktop framework to a MAUI cross-plataform