using System;

namespace VisualLogic.Exceptions;

public class GlobalVariableException : Exception
{
    public override string Message => 
        "Global Variables can't be captured in dependecy injection process.\n" +
        "Try use parameters with default values instead.";
}