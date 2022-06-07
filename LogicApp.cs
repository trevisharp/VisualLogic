using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VisualLogic;

using Exceptions;

public abstract class LogicApp
{
    private DependecyInjectionManager man = null;
    private VisualScreen screen = null;
    private List<(HookType hook, string func)> hooks = new List<(HookType, string)>();
    public int Fps { get; set; } = 25;
    protected abstract DIBuilder DefineDependencyInjection();
    protected abstract void LoadFromParams(params object[] args);
    protected abstract void SetRunHooks();
    public void AddRunHook(string function, HookType hook)
        => this.hooks.Add((hook, function));
    public async Task CallHookAsync(HookType hook)
    {
        var funcs = getfuncs(hook);
        foreach (var func in funcs)
            await man.RunAsync(func);
    }
    public static void Run(int fps, params object[] args)
    {
        var app = getapp();
        if (app == null)
            throw new InexistentLogicAppException();
        app.LoadFromParams(args);
        app.man = app.DefineDependencyInjection().Build();
        var screen = new VisualScreen(app);
        screen.Open();
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

    private IEnumerable<string> getfuncs(HookType hook)
        => hooks.Where(t => t.hook == hook).Select(t => t.func);
}