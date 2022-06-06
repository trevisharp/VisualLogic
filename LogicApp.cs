using System;
using System.Reflection;

namespace VisualLogic;

using Exceptions;

public abstract class LogicApp
{
    private DependecyInjectionManager man = null;
    public int Fps { get; set; }
    protected abstract DIBuilder DefineDependencyInjection();
    protected abstract void LoadFromParams(params object[] args);
    public void Run()
    {
        VisualScreen screen = new VisualScreen();
        screen.TimerDelay = Fps;

        screen.Open();
    }

    public static void Run(int fps, params object[] args)
    {
        var app = getapp();
        if (app == null)
            throw new InexistentLogicAppException();
        app.LoadFromParams(args);
        app.man = app.DefineDependencyInjection().Build();
    }

    private static LogicApp getapp()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in types)
        {
            if (type.BaseType != typeof(LogicApp))
                continue;
            return type.GetConstructor(new Type[0]).Invoke(null) as LogicApp;
        }
        return null;
    }
}