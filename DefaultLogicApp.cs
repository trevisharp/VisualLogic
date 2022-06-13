namespace VisualLogic;
public class DefaultLogicApp : LogicApp
{
    protected override void DefineDependencyInjection(DIBuilder builder)
    {
        builder.AddMethod("logic");
    }
}