using System;
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
        this.instances.Add(def);
        return this;
    }

    public void Run()
    {
        List<object> parameters = new List<object>();
        List<InstanceDefinition> objects = new List<InstanceDefinition>(instances);
        foreach (var param in Method.GetParameters())
        {
            object obj = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].Type == param.ParameterType)
                {
                    obj = objects[i].Value;
                    objects.RemoveAt(i);
                    break;
                }
            }
            if (obj == null)
            {
                var constructor = param.ParameterType.GetConstructor(new Type[0]);
                if (constructor == null)
                    throw new InexistentInstanceDefinitionException(param.ParameterType);
                obj = constructor.Invoke(null);
            }
            parameters.Add(obj);
        }
        Method.Invoke(null, parameters.ToArray());
    }
}


