using Serilog.Context;
using Serilog.Events;
using System.Diagnostics;
using System.Reflection;
using uBeac.Interception;
using Serilog;
using uBeac.Web.Logging;

namespace TestApplication
{
    public interface ITestService
    {
        int Add(int x, int y);
        TestModel Send(TestModel model);
    }

    public class TestService : ITestService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
        public TestModel Send(TestModel model)
        {
            model.Id = new Random().Next(1000);
            return model;
        }
    }


    public class TestLogInterceptor : IInterceptor
    {
        Stopwatch stopWatch;
        private readonly MethodLogs _methodLogs;
        public TestLogInterceptor(MethodLogs methodLogs)
        {
            _methodLogs = methodLogs;            
        }

        public void AfterInvoke(MethodInfo method, object invokedResult, object[] parameters)
        {
            stopWatch.Stop();
            var log = new MethodLog
            {
                Method = method.Name,
                Class = method.DeclaringType.Name,
                Duration = stopWatch.Elapsed.TotalMilliseconds
            };

            _methodLogs.Add(log);

            //LogContext.PushProperty("method", method.Name);
            //LogContext.PushProperty("duration", duration);
            //LogContext.PushProperty("class", method.DeclaringType.Name);


            //var log = new MethodLog
            //{
            //    Method = method.Name,
            //    Type = method.DeclaringType?.FullName ?? string.Empty,
            //    Duration = duration
            //};
            //LogContext.PushProperty("{@Methods}", log);
        }

        public void BeforeInvoke(MethodInfo method, object[] parameters)
        {
            stopWatch = Stopwatch.StartNew();
        }

        public bool Skip()
        {
            return false;
        }
    }
   

}
