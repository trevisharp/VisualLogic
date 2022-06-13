namespace VisualLogic;

using Elements;

public class DefaultLogicApp : LogicApp
{
    protected override void DefineDependencyInjection(DIBuilder builder)
    {
        builder.AddMethod("logic")
            .AddInstance(new vArray(0, 100, 20))
            .AddInstance(new vRandomArray(0, 100, 20));
    }

    protected override void LoadFromParams(params object[] args)
    {
        
    }

    protected override void SetRunHooks()
    {
        AddRunHook("logic", HookType.OnAppStart);
    }
}