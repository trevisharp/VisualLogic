namespace VisualLogic;

public class DefaultLogicApp : LogicApp
{
    protected override DIBuilder DefineDependencyInjection(DIBuilder builder)
    {
        builder.AddMethod("logic");
        return builder;
    }

    protected override void LoadFromParams(params object[] args)
    {
        
    }

    protected override void SetRunHooks()
    {
        AddRunHook("logic", HookType.OnAppStart);
    }
}