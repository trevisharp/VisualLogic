using System;

namespace VisualLogic.Exceptions;

public class InexistentInstanceDefinitionException : Exception
{
    private Type type;
    public InexistentInstanceDefinitionException(Type type)
        => this.type = type;
    public override string Message => $"Do not exist a {this.type} instance definition.";
}