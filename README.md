# VisualLogic
A .NET framework to create educational applications and realize algorithms concepts proof.

[![](https://img.shields.io/badge/Visual-Logic-purple?style=for-the-badge)](https://github.com/trevisharp/VisualLogic)
[![](https://img.shields.io/nuget/dt/VisualLogic?color=purple&style=for-the-badge)](https://www.nuget.org/packages/VisualLogic/1.1.0)
[![](https://img.shields.io/github/license/Trevisharp/VisualLogic?color=purple&style=for-the-badge)](LICENSE)
[![](https://img.shields.io/github/last-commit/Trevisharp/VisualLogic?color=purple&style=for-the-badge)](https://github.com/trevisharp/VisualLogic/commits/main)
[![](https://img.shields.io/github/commit-activity/m/Trevisharp/VisualLogic?color=purple&style=for-the-badge)](https://github.com/trevisharp/VisualLogic/commits/main)

``` cs
using VisualLogic;
using VisualLogic.Elements;

public class SortLogicApp : LogicApp
{
    public int ArrayLength { get; set; }
    protected override DIBuilder DefineDependencyInjection()
    {
        DIBuilder builder = new DIBuilder();
        return builder
            .AddMethod("solution")
            .AddInstance<VisualArray>(new VisualArray(100, 1100, ArrayLength));
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
