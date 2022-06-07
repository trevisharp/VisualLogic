using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisualLogic;

using Exceptions;

public class DependecyInjectionManager
{
    internal class InstanceDefinition
    {
        public Type Type { get; set; }
        public object Value { get; set; }
        public Predicate<string> Condition { get; set; }
    }
    
    #region Private Fields
    
    private Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
    private List<InstanceDefinition> instances = new List<InstanceDefinition>();

    #endregion

    #region Public Methods

    public void AddInstance<T>(T value, Predicate<string> condition = null)
    {
        if (condition == null)
            condition = s => true;
        InstanceDefinition def = new InstanceDefinition();
        def.Type = typeof(T);
        def.Value = value;
        def.Condition = condition;
        this.instances.Add(def);
    }
    
    public void AddMethod(string name)
    {
        var types = Assembly.GetEntryAssembly().GetTypes();
        foreach (var type in types)
        {
            var lwname = type.Name.ToLower();
            if (!lwname.Contains("program"))
                continue;
            addmethod(type, name);
            return;
        }
    }

    public void RunAll()
    {
        foreach (var method in methods)
            run(method.Value);
    }

    public void Run(string name)
    {
        if (name == null || !methods.ContainsKey(name))
            throw new Exception("Invalid method name");
        run(methods[name]);
    }

    public async Task RunAsync(string name)
    {
        await Task.Factory.StartNew(() =>
        {
            Run(name);
        });
    }
    
    #endregion

    #region Private Methods

    public async Task RunAllAsync()
    {
        await Task.Factory.StartNew(() =>
        {
            RunAll();
        });
    }

    private void run(MethodInfo method)
    {
        List<object> parameters = new List<object>();
        foreach (var param in method.GetParameters())
        {
            var obj = getparam(param);
            if (obj == null)
                throw new InexistentInstanceDefinitionException(param.ParameterType);
            parameters.Add(obj);
        }
        method.Invoke(null, parameters.ToArray());
    }

    private object getparam(ParameterInfo param)
    {
        foreach (var instance in this.instances)
        {
            if (instance.Type == param.ParameterType 
                && instance.Condition(param.Name))
                return instance.Value;
        }
        return null;
    }

    private void addmethod(Type type, string name)
    {
        foreach (var func in type.GetRuntimeMethods())
        {
            var lwname = func.Name.ToLower();
            string expected = "<<main>$>g__" + name.ToLower();
            if (lwname.Length < expected.Length ||
                lwname.Substring(0, expected.Length) != expected)
                continue;
            this.methods.Add(name, func);
            return;
        }
    }

    #endregion
}