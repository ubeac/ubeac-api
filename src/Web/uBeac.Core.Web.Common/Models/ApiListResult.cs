namespace uBeac.Web
{
    public interface IApiListResult<TResult> : IApiResult<IEnumerable<TResult>>
    {
        public int PageSize { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }
        public long TotalCount { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
    }

    public class ApiListResult<TResult> : ApiResult<IEnumerable<TResult>>, IApiListResult<TResult>
    {
        public int PageSize { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }
        public long TotalCount { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }

        public ApiListResult(IEnumerable<TResult> items) : base(items)
        {
            var count = items.Count();
            PageNumber = 1;
            PageSize = count;
            TotalCount = count;
            HasPrevious = false;
            HasNext = false;
            TotalPages = 1;
        }

        public ApiListResult(Exception exception) : base(exception)
        {
        }
    }
}
