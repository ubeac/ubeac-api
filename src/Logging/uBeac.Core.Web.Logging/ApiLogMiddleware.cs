using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;

namespace uBeac.Web.Logging
{
    public class MethodLogs
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MethodLogs(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
            if (!_httpContextAccessor.HttpContext.Items.ContainsKey("MethodLogs"))
                _httpContextAccessor.HttpContext.Items.Add("MethodLogs", new List<MethodLog>());

        }
        public void Add(MethodLog log)
        {
            var x = (List<MethodLog>)_httpContextAccessor.HttpContext.Items["MethodLogs"];
            x.Add(log);
        }

        public List<MethodLog> GetMethodLogs()
        {
            return (List<MethodLog>)_httpContextAccessor.HttpContext.Items["MethodLogs"];
        }
    }
    public class MethodLog
    {
        public string Method { get; set; }
        public string Class { get; set; }
        public double Duration { get; set; }
    }

    public class ApiLogMiddleware
    {
        private readonly RequestDelegate next;

        public ApiLogMiddleware(RequestDelegate next)
        {
            this.next = next;
            //_methodLogs = methodLogs;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Exception exception = default;
            var log = new ApiLog()
            {
                Date = DateTime.Now,
                Request = CreateRequestInstance(context),
                Response = new LogResponse(),
                TraceId = context.TraceIdentifier
            };

            var contentLength = new ContentLengthTracker();
            context.Response.Body = new ContentLengthTrackingStream(context.Response.Body, contentLength);

            try
            {
                await next(context);
                log.Response.StatusCode = context.Response.StatusCode;
            }
            catch (Exception ex)
            {
                exception = ex;
                log.Response.StatusCode = 500;
                throw;
            }
            finally
            {
                log.Response.Length = contentLength.ContentLength;
                log.Duration = (DateTime.Now - log.Date).TotalMilliseconds;
                WriteLog(log, exception, context);
            }
        }


        private void WriteLog(ApiLog log, Exception ex, HttpContext context)
        {
            var logger = Log.ForContext("api", log);

            var _methodLogs = context.Items.ContainsKey("MethodLogs") ? (List<MethodLog>)context.Items["MethodLogs"] : new List<MethodLog>();

            LogContext.PushProperty("tttt", 123);
            if (logger == null || log == null)
                return;

            //var logContent = "";// $"Duration: {log.Duration}ms, TraceId: {log.TraceId}, RequestLength: { log.Request?.Length} ";

            LogLevel logLevel = LogLevel.Information;

            if (log.Response?.StatusCode < 500 && log.Response.StatusCode >= 400)
                logLevel = LogLevel.Warning;

            if (log.Response?.StatusCode >= 500)
                logLevel = LogLevel.Error;

            switch (logLevel)
            {
                case LogLevel.Information:
                    logger.Information("{@Trace}, {@ALIALIALIALI}", log, _methodLogs);
                    return;

                case LogLevel.Warning:
                    logger.Warning("{@Trace}, {@ALIALIALIALI}", log, _methodLogs);
                    return;

                case LogLevel.Error:
                    logger.Error("{@Trace}, {@ALIALIALIALI}", log, _methodLogs);
                    return;

                default:
                    logger.Fatal("{@Trace}, {@ALIALIALIALI}", log, _methodLogs);
                    return;
            }
        }

        private static LogRequest CreateRequestInstance(HttpContext context)
        {
            var request = context.Request;
            return new LogRequest()
            {
                Method = request.Method,
                ContentType = request.ContentType,
                ContentLength = request.ContentLength,
                Host = request.Host.Host,
                Port = request.Host.Port,
                Scheme = request.Scheme,
                QueryString = request.QueryString.ToString(),
                Protocol = request.Protocol,
                HasFormContentType = request.HasFormContentType,
                Path = request.Path.Value,
                Headers = request.Headers.ToDictionary(item => item.Key, item => item.Value.ToList()),
                Length = request.ContentLength,
                Ip = context.Connection.RemoteIpAddress.ToString(),
                //UserId = applicationContext.User.UserId,
                Username = context.User.Identity.Name
            };
        }
    }

}
