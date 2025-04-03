using System.Collections.Frozen;

namespace ServiceLocatorThingie;
internal static class Services
{
    private static Dictionary<Type, object>? _registry;
    private static FrozenDictionary<Type, object>? registry;

    public static bool IsFinal { get; private set; } = true;
    
    public static bool TryRegister<T>(T service) where T : class
    {
        if (IsFinal)
            throw new ServiceLocatorNotOpenException();
        return _registry!.TryAdd(typeof(T), service);
    }

    public static void ForceAdd<T>(T service) where T : class
    {
        if (IsFinal)
            throw new ServiceLocatorNotOpenException();

        _registry![typeof(T)] = service;
    }

    public static bool TryLocate<T>(out T? t)
    {
        if (!IsFinal)
            throw new ServiceLocatorNotClosedException();

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
        
        _registry = registry is null ? [] : registry.ToDictionary();
        registry = null;
    }
    public static void Close()
    {
        IsFinal = true;
        
        registry = _registry!.ToFrozenDictionary();
        _registry = null;
    }
}

internal class ServiceLocatorNotClosedException : Exception;
internal class ServiceLocatorNotOpenException : Exception;