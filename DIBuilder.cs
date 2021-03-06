namespace VisualLogic;

public class DIBuilder
{
    private DependecyInjectionManager man = new DependecyInjectionManager();

    public DIMethod AddMethod(string name)
        => man.AddMethod(name);

    internal DependecyInjectionManager Build()
    {
        var finalmanager = this.man;
        this.man = null;
        return finalmanager;
    }
}