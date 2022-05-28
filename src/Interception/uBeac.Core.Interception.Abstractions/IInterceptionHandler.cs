using System.Reflection;

namespace uBeac.Interception;

public interface IInterceptionHandler<T>
{
    IEnumerable<IInterceptor<T>> Interceptors { get; }

    object Invoke(object instance, MethodInfo targetMethod, object[] args);
}