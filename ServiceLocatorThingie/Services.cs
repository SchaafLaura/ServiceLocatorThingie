using System.Collections.Frozen;

namespace ServiceLocatorThingie;
internal static class Services
{
    private static Dictionary<Type, object>? _registry = [];
    private static FrozenDictionary<Type, object>? registry;

    public static bool IsFinal = true;
    
    public static bool TryRegister<T>(T service) where T : class
    {
        return !IsFinal && _registry!.TryAdd(typeof(T), service);
    }

    public static void ForceAdd<T>(T service) where T : class
    {
        if (IsFinal)
            return;

        _registry![(typeof(T))] = service;
    }

    public static bool TryLocate<T>(out T? t)
    {
        if (!IsFinal)
        {
            t = default;
            return false;
        }

        t = default;
        var ret = registry!.TryGetValue(typeof(T), out var x);
        if (!ret)
            return false;
        t = (T) x!;
        return true;
    }

    public static void Open()
    {
        IsFinal = false;
    }
    public static void Close()
    {
        registry = _registry!.ToFrozenDictionary();
        IsFinal = true;
    }
}