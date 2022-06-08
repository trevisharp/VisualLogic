using System;

namespace VisualLogic;

internal class InstanceDefinition
{
    public Type Type { get; set; }
    public object Value { get; set; }
    public Predicate<(string name, Type type, int index)> Condition { get; set; }
}