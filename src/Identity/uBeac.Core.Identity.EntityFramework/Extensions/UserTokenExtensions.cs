using Microsoft.EntityFrameworkCore;
using uBeac.Identity;
using uBeac.Identity.EntityFramework;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserTokenExtensions
{
    public static IServiceCollection AddEFUserTokenRepository<TContext, TUserKey>(this IServiceCollection services)
        where TContext : DbContext
        where TUserKey : IEquatable<TUserKey>
    {
        services.AddScoped<IUserTokenRepository<TUserKey>, EFUserTokenRepository<TUserKey, TContext>>();
        return services;
    }

    public static IServiceCollection AddEFUserTokenRepository<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped<IUserTokenRepository, EFUserTokenRepository<TContext>>();
        return services;
    }
}