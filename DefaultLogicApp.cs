namespace VisualLogic;
internal class DefaultLogicApp : LogicApp
{
    protected override void DefineDependencyInjection(DIBuilder builder)
    {
        builder.AddMethod("logic");
    }
}