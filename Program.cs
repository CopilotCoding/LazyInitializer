using static LazyFactory;

class Program
{
    static void Main()
    {
        var myLazyString1 = GetOrCreate(() => "Hello, world!");
        Console.WriteLine(myLazyString1); // Output: Hello, world!

        var myLazyString2 = GetOrCreate(() => "Goodbye, world!");
        Console.WriteLine(myLazyString2); // Output: Hello, world! (same as myLazyString1)

        var myLazyInt = GetOrCreate(() => 42);
        Console.WriteLine(myLazyInt); // Output: 42

        var myLazyObject = GetOrCreate(() => new object());
        Console.WriteLine(myLazyObject); // Output: MyObject
    }
}

class Lazy<T>
{
    private T _value;
    private Func<T> _initializer;

    public Lazy(Func<T> initializer)
    {
        _initializer = initializer;
    }

    public T Value => _value ?? (_value = _initializer());
}

static class LazyFactory
{
    private static Dictionary<string, object> _lazyObjects = new Dictionary<string, object>();

    public static T GetOrCreate<T>(Func<T> initializer)
    {
        var key = typeof(T).FullName;
        return Create(key, initializer).Value;
    }

    public static Lazy<T> Create<T>(string key, Func<T> initializer)
    {
        if (!_lazyObjects.ContainsKey(key))
        {
            _lazyObjects[key] = new Lazy<T>(initializer);
        }
        return (Lazy<T>)_lazyObjects[key];
    }
}
