namespace uBeac;

public static class ResultExtensions
{
    public static IListResult<T> ToApiListResult<T>(this IEnumerable<T> values)
    {
        return new ListResult<T>(values);
    }

    public static IResult<T> ToApiResult<T>(this T value)
    {
        return new Result<T>(value);
    }

    public static IListResult<T> ToApiListResult<T>(this Exception exception)
    {
        return new ListResult<T>(exception);
    }

    public static IResult<T> ToApiResult<T>(this Exception exception)
    {
        return new Result<T>(exception);
    }
}