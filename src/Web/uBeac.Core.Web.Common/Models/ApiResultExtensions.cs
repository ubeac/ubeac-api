namespace uBeac.Web
{
    public static class ApiResultExtensions
    {
        public static IApiListResult<T> ToApiListResult<T>(this ICollection<T> values)
        {
            return new ApiListResult<T>(values);
        }

        public static IApiResult<T> ToApiResult<T>(this T value)
        {
            return new ApiResult<T>(value);
        }

        public static IApiListResult<T> ToApiListResult<T>(this Exception exception)
        {
            return new ApiListResult<T>(exception);
        }

        public static IApiResult<T> ToApiResult<T>(this Exception exception)
        {
            return new ApiResult<T>(exception);
        }
    }
}
