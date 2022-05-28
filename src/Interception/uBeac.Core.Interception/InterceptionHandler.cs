using System.Reflection;

namespace uBeac.Interception;

public class InterceptionHandler<T> : IInterceptionHandler<T>
{
    public IEnumerable<IInterceptor<T>> Interceptors { get; }

    public InterceptionHandler(IEnumerable<IInterceptor<T>> interceptors)
    {
        Interceptors = interceptors;
    }

    public object Invoke(object instance, MethodInfo targetMethod, object[] args)
    {
        try
        {
            OnExecuting(args);

            var result = targetMethod.Invoke(instance, args);

            OnExecuted(result);

            return result;
        }
        catch (Exception e)
        {
            OnException(e);

            if (e.InnerException != null) throw e.InnerException;
            throw;
        }
    }

    private void OnExecuting(object[] args)
    {
        foreach (var interceptor in Interceptors) interceptor.OnExecuting(args);
    }

    private void OnExecuted(object result)
    {
        foreach (var interceptor in Interceptors) interceptor.OnExecuted(result);
    }

    private void OnException(Exception exception)
    {
        foreach (var interceptor in Interceptors) interceptor.OnException(exception);
    }
}