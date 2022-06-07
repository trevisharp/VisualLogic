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
    protected override DIBuilder DefineDependencyInjection(DIBuilder builder)
    {
        return builder
            .AddMethod("solution")
            .AddInstance(new VisualArray(100, 1100, ArrayLength));
    }

    protected override void LoadFromParams(params object[] args)
    {
        this.ArrayLength = (int)args[0];
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
using VisualLogic.Elements;

SortLogicApp.Run(25, 40);

void solution(VisualArray array)
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

Uma aplicação com a solution vazia pode ser entregue ao usuário. Lá ele pode implementar e testar a sua solução enquanto varia os parâmetros de Delay de apresentação e tamanho do Array.
É importante perceber que é possível implementar heranças de VisualElement possibilitando diversos tipos de desafios ou utilizar um objeto existente na VisualLogic.Elements.

## TODO

- Change Tutorial to English
- Add more default Elements
- Improve DependecyInjection abstraction