using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VisualLogic;

public class DependecyInjectionManager
{    
    private List<DIMethod> methods = new List<DIMethod>();
    
    public DIMethod AddMethod(string name)
    {
        var types = Assembly.GetEntryAssembly().GetTypes();
        foreach (var type in types)
        {
            var lwname = type.Name.ToLower();
            if (!lwname.Contains("program"))
                continue;
            var method = getmethod(type, name);
            if (method != null)
            {
                methods.Add(method);
                return method;
            }
        }
        return null;
    }

    public void RunAll()
    {
        foreach (var method in methods)
            method.Run();
    }

    public void Run(string name)
    {
        var method = methods.FirstOrDefault(m => m.Function == name);
        if (method is null)
            throw new Exception("Invalid method name");
        method.Run();
    }

    public async Task RunAsync(string name)
    {
        await Task.Factory.StartNew(() =>
        {
            Run(name);
        });
    }
    
    public async Task RunAllAsync()
    {
        await Task.Factory.StartNew(() =>
        {
            RunAll();
        });
    }

    private DIMethod getmethod(Type type, string name)
    {
        foreach (var func in type.GetRuntimeMethods())
        {
            var lwname = func.Name.ToLower();
            string expected = "<<main>$>g__" + name.ToLower();
            if (lwname.Length < expected.Length ||
                lwname.Substring(0, expected.Length) != expected)
                continue;
            DIMethod method = new DIMethod();
            method.Parent = this;
            method.Function = name;
            method.Method = func;
            return method;
        }
        return null;
    }
}