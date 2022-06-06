using System;

namespace VisualLogic.Exceptions;

public class InexistentLogicAppException : Exception
{
    public override string Message => "This app needs a class that inherits LogicApp.";
}