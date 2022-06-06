using System;

namespace VisualLogic;

public class DIBuilder
{
    private DependecyInjectionManager man = new DependecyInjectionManager();

    public DIBuilder AddInstance<T>(T value, Predicate<string> condition = null)
    {
        man.AddInstance<T>(value, condition);
        return this;
    }

    public DIBuilder AddMethod(string name)
    {
        man.AddMethod(name);
        return this;
    }

    public DependecyInjectionManager Build()
    {
        var finalmanager = this.man;
        this.man = null;
        return finalmanager;
    }
}