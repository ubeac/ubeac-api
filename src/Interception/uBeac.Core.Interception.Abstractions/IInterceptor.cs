using System.Reflection;

namespace uBeac.Interception
{
    public interface IInterceptor
    {
        void BeforeInvoke(MethodInfo method, object[] parameters);
        void AfterInvoke(MethodInfo method, object invokedResult, object[] parameters);
        bool Skip();
    }
}
