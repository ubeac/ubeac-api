using uBeac.Interception;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static InterceptionBuilder<TInterface> Intercept<TInterface>(this IServiceCollection services)
            where TInterface : class
        {
            return new InterceptionBuilder<TInterface>(services);
        }
    }
}
