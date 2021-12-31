using Microsoft.AspNetCore.Http;

namespace uBeac.Web
{
    public interface IApiResult
    {
        List<Error> Errors { get; }
        string TraceId { get; set; }
        double Duration { get; set; }
        int Code { get; set; }
    }

    public interface IApiResult<TData> : IApiResult
    {
        TData Data { get; }
    }

    public class ApiResult : IApiResult
    {
        public List<Error> Errors { get; } = new List<Error>();
        public string TraceId { get; set; } = string.Empty;
        public double Duration { get; set; } = 0;
        public int Code { get; set; } = StatusCodes.Status200OK;
        public ApiResult()
        {

        }
        public ApiResult(Exception exception)
        {
            Errors.Add(new Error(exception));
            Code = StatusCodes.Status500InternalServerError;
        }
    }

    public class ApiResult<TData> : ApiResult, IApiResult<TData>
    {
        public TData Data { get; }
        public ApiResult(TData data)
        {
            Data = data;
        }
        public ApiResult(Exception exception) : base(exception)
        {
        }
    }
}
