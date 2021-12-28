using Microsoft.AspNetCore.Http;

namespace uBeac.Web
{
    public interface IResultSet
    {
        List<Error> Errors { get; }
        string TraceId { get; set; }
        int Duration { get; set; }
        int Code { get; set; }
    }

    public interface IResultSet<TData> : IResultSet
    {
        TData Data { get; }
    }

    public class ResultSet : IResultSet
    {
        public List<Error> Errors { get; } = new List<Error>();
        public string TraceId { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public int Code { get; set; } = StatusCodes.Status200OK;
    }

    public class ResultSet<TData> : ResultSet, IResultSet<TData>
    {
        public TData Data { get; }
        public ResultSet(TData data)
        {
            Data = data;
        }
    }
}
