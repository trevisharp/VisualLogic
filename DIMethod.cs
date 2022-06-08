using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace VisualLogic;

using Exceptions;

public class DIMethod
{
    internal DependecyInjectionManager Parent { get; set; }
    public string Function { get; set; }
    public MethodInfo Method { get; set; }
    private List<InstanceDefinition> instances = new List<InstanceDefinition>();

    public DIMethod AddInstance<T>(T value)
    {
        InstanceDefinition def = new InstanceDefinition();
        def.Type = typeof(T);
        def.Value = value;
        def.Condition = t => t.type == def.Type;
        this.instances.Add(def);
        return this;
    }

    public void Run()
    {
        List<object> parameters = new List<object>();
        int i = 0;
        foreach (var param in Method.GetParameters())
        {
            var obj = getparam(param, i++);
            if (obj == null)
                throw new InexistentInstanceDefinitionException(param.ParameterType);
            parameters.Add(obj);
        }
        Method.Invoke(null, parameters.ToArray());
    }

    private object getparam(ParameterInfo param, int index)
        => instances
            .FirstOrDefault(
                p => p.Condition((param.Name, param.ParameterType, index)))
            .Value;
}


