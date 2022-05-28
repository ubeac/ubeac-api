using System.Reflection;
using uBeac.Interception;

namespace API;

public class AccountsControllerInterceptor : IInterceptor<IUserService<User>>
{
    public MethodInfo TargetMethod { get; } = typeof(IUserService<User>).GetMethod(nameof(IUserService<User>.Authenticate));

    public void OnExecuting(object[] args)
    {
        Console.WriteLine("On Executing");
    }

    public void OnExecuted(object result)
    {
        Console.WriteLine("On Executed");
    }

    public void OnException(Exception exception)
    {
        Console.WriteLine("On Exception");
    }
}