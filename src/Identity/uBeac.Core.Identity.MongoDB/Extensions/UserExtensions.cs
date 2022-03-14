using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserExtensions
{
    public static IServiceCollection AddMongoDBUserRepository<TMongoDbContext, TUserKey, TUser>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.TryAddScoped<IUserRepository<TUserKey, TUser>, MongoUserRepository<TUserKey, TUser>>();
        services.TryAddScoped<IUserRepository<TUserKey, TUser>>(provider =>
        {
            var dbContext = provider.GetRequiredService<TMongoDbContext>();
            var appContext = provider.GetRequiredService<IApplicationContext>();

            return new MongoUserRepository<TUserKey, TUser>(appContext, dbContext);
        });

        return services;
    }

    public static IServiceCollection AddMongoDBUserRepository<TMongoDbContext, TUser>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUser : User
    {
        services.TryAddScoped<IUserRepository<TUser>>(provider =>
        {
            var dbContext = provider.GetRequiredService<TMongoDbContext>();
            var appContext = provider.GetRequiredService<IApplicationContext>();

            return new MongoUserRepository<TUser>(appContext, dbContext);
        });

        return services;
    }
}