using uBeac.Identity;
using uBeac.Identity.EntityFramework;
using uBeac.Repositories.EntityFramework;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserExtensions
{
    public static IServiceCollection AddEFUserRepository<TContext, TUserKey, TUser>(this IServiceCollection services)
        where TContext : EFDbContext
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.AddScoped<IUserRepository<TUserKey, TUser>, EFUserRepository<TUserKey, TUser, TContext>>();
        return services;
    }

    public static IServiceCollection AddEFUserRepository<TContext, TUser>(this IServiceCollection services)
        where TContext : EFDbContext
        where TUser : User
    {
        services.AddScoped<IUserRepository<TUser>, EFUserRepository<TUser, TContext>>();
        return services;
    }
}