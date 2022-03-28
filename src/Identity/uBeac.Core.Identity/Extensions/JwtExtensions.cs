using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtProvider(this IServiceCollection services, JwtOptions options)
    {
        services.AddSingleton(options);
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        return services;
    }
}