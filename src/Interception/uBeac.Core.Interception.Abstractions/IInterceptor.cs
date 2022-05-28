using System.Reflection;

namespace uBeac.Interception;

public interface IInterceptor<T>
{
    MethodInfo TargetMethod { get; }

    void OnExecuting(object[] args);
    void OnExecuted(object result);
    void OnException(Exception exception);
}