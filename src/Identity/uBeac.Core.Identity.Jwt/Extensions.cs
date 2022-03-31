using Microsoft.Extensions.Configuration;
using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddJwtService<TUserKey, TUser>(this IServiceCollection services,
        IConfiguration config)
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.Configure<JwtOptions>(config.GetSection("Jwt"));
        services.AddScoped<ITokenService<TUserKey, TUser>, JwtTokenService<TUserKey, TUser>>();
        services.AddScoped<IJwtTokenService<TUserKey, TUser>, JwtTokenService<TUserKey, TUser>>();

        return services;
    }

    public static IServiceCollection AddJwtService<TUser>(this IServiceCollection services, IConfiguration config)
        where TUser : User
    {
        services.Configure<JwtOptions>(config.GetSection("Jwt"));
        services.AddScoped<ITokenService<TUser>, JwtTokenService<TUser>>();
        services.AddScoped<IJwtTokenService<TUser>, JwtTokenService<TUser>>();

        return services;
    }
}