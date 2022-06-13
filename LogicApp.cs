using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VisualLogic;

public abstract class LogicApp
{
    private DependecyInjectionManager man = null;
    private List<(HookType hook, string func)> hooks = new List<(HookType, string)>();
    public int Fps { get; set; } = 25;
    
    protected abstract void DefineDependencyInjection(DIBuilder builder);
    protected abstract void LoadFromParams(AppArgs args);
    protected virtual void SetRunHooks()
    {
        var method = man.Methods.FirstOrDefault();
        if (method == null) return;
        string functionname = method.Function.Clone() as string; //Copy to avoid thread problems
        AddRunHook(functionname, HookType.OnAppStart);
    }
    
    public void AddRunHook(string function, HookType hook)
        => this.hooks.Add((hook, function));
    
    public async Task CallHookAsync(HookType hook)
    {
        var funcs = getfuncs(hook);
        foreach (var func in funcs)
            await man.RunAsync(func);
    }
    
    public static void Run(int delay, params object[] args)
    {
        var app = getapp();
        if (app != null)
        {
            AppArgs appargs = new AppArgs(args);
            app.LoadFromParams(appargs);
            DIBuilder builder = new DIBuilder();
            app.DefineDependencyInjection(builder);
            app.man = builder.Build();
            app.SetRunHooks();
        }
        var screen = new VisualScreen(app);
        screen.Delay = delay;
        screen.Open();
    }
    
    private static LogicApp getapp()
    {
        var types = Assembly.GetEntryAssembly().GetTypes();
        foreach (var type in types)
        {
            if (type.BaseType != typeof(LogicApp))
                continue;
            return type.GetConstructor(new Type[0]).Invoke(null) as LogicApp;
        }
        return new DefaultLogicApp();
    }

    private IEnumerable<string> getfuncs(HookType hook)
        => hooks.Where(t => t.hook == hook).Select(t => t.func);
}