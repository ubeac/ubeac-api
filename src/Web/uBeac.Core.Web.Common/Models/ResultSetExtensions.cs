namespace uBeac.Web
{
    public static class ResultSetExtensions
    {
        public static IListResultSet<T> ToListResultSet<T>(this ICollection<T> values)
        {
            return new ListResultSet<T>(values);
        }

        public static IResultSet<T> ToResultSet<T>(this T value)
        {
            return new ResultSet<T>(value);
        }
    }
}
