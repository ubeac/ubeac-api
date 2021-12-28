namespace uBeac.Web
{
    public interface IListResultSet<TResult> : IResultSet<ICollection<TResult>>
    {
        public int PageSize { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }
        public long TotalCount { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
    }

    public class ListResultSet<TResult> : ResultSet<ICollection<TResult>>, IListResultSet<TResult>
    {
        public int PageSize { get; }
        public int TotalPages { get; }
        public int PageNumber { get; }
        public long TotalCount { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }

        public ListResultSet(ICollection<TResult> items) : base(items)
        {
            PageNumber = 1;
            PageSize = items.Count;
            TotalCount = items.Count;
            HasPrevious = false;
            HasNext = false;
            TotalPages = 1;
        }
    }
}
