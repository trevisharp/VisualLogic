namespace VisualLogic;

public class AppArgs
{
    private object[] args;

    public AppArgs(object[] args)
        => this.args = args;

    public object this[int i, object def = null]
        => i < args.Length ? args[i] : def;

    public T get<T>(int i, T def)
        => this[i, def] is T obj ? obj : def;

    public T get<T>(int i) 
        => get(i, default(T));
}