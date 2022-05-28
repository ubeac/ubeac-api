using System.Reflection;

namespace uBeac.Interception;

public class InterceptionDispatchProxy<T> : DispatchProxy
{
    private T _instance;
    private IInterceptionHandler<T> _handler;

    public static T Create(T instance, IInterceptionHandler<T> handler)
    {
        var proxy = Create<T, InterceptionDispatchProxy<T>>();

        if (proxy is InterceptionDispatchProxy<T> interceptionDispatchProxy)
        {
            interceptionDispatchProxy._instance = instance;
            interceptionDispatchProxy._handler = handler;
        }

        return proxy;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args) => _handler.Invoke(_instance, targetMethod, args);
}