using System.Reflection;

namespace uBeac.Interception
{
    public class Proxy<T> : DispatchProxy
    {
        public T Target { get; set; }
        public List<IInterceptor> Interceptors { get; set; }

        public static T Decorate(T target, List<IInterceptor> interceptors)
        {
            var proxy = Create<T, Proxy<T>>();
            (proxy as Proxy<T>).Target = target;
            (proxy as Proxy<T>).Interceptors = interceptors;
            return proxy;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                if (!IsReturnTypeTask(targetMethod))
                {
                    return ExecuteSyncInternal(targetMethod, args);
                }
                else if (!IsReturnTypeTaskWithResult(targetMethod))
                {
                    return ExecuteAsyncInternal(targetMethod, args);
                }
                else
                {
                    return ExecuteAsyncWithResultInternal(targetMethod, args);
                }
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException;
            }
        }

        private object ExecuteSyncInternal(MethodInfo targetMethod, object[] args)
        {
            object result = null;

            var skipInvoke = false;
            foreach (var interceptor in Interceptors)
            {
                interceptor.BeforeInvoke(targetMethod, args);
                if (interceptor.Skip()) skipInvoke = true;
            }

            if (!skipInvoke)
            {
                result = targetMethod.Invoke(Target, args);
            }

            foreach (var interceptor in Interceptors)
            {
                interceptor.AfterInvoke(targetMethod, result, args);
            }

            return result;

        }

        private async Task ExecuteAsyncInternal(MethodInfo targetMethod, object[] args)
        {
            Task result = null;

            var skipInvoke = false;
            foreach (var interceptor in Interceptors)
            {
                interceptor.BeforeInvoke(targetMethod, args);
                if (interceptor.Skip()) skipInvoke = true;
            }

            if (!skipInvoke)
            {
                result = (Task)targetMethod.Invoke(Target, args);
                await result;
            }

            foreach (var interceptor in Interceptors)
            {
                interceptor.AfterInvoke(targetMethod, result, args);
            }

        }

        private object ExecuteAsyncWithResultInternal(MethodInfo targetMethod, object[] args)
        {
            Task result = null;

            var skipInvoke = false;
            foreach (var interceptor in Interceptors)
            {
                interceptor.BeforeInvoke(targetMethod, args);
                if (interceptor.Skip()) skipInvoke = true;
            }

            if (!skipInvoke)
            {
                result = (Task)targetMethod.Invoke(Target, args);
                result.Wait();
            }

            foreach (var interceptor in Interceptors)
            {
                interceptor.AfterInvoke(targetMethod, result, args);
            }

            return result;

        }

        public static bool IsReturnTypeTask(Type returnType)
        {
            if (returnType == typeof(Task))
            {
                return true;
            }

            if (returnType.BaseType != null)
            {
                return IsReturnTypeTask(returnType.BaseType);
            }

            return false;
        }

        public static bool IsReturnTypeTask(MethodBase method)
        {
            var methodInfo = (MethodInfo)method;
            var returnType = methodInfo.ReturnType;
            return IsReturnTypeTask(returnType);
        }

        public static bool IsReturnTypeTaskWithResult(MethodBase method)
        {
            var methodInfo = (MethodInfo)method;
            var returnType = methodInfo.ReturnType;
            return IsReturnTypeTask(returnType) && returnType != typeof(Task);
        }
    }
}
