using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserTokenExtensions
{
    public static IServiceCollection AddMongoDBUserTokenRepository<TMongoDbContext, TUserKey>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUserKey : IEquatable<TUserKey>
    {
        services.TryAddScoped<IUserTokenRepository<TUserKey>, MongoUserTokenRepository<TUserKey, TMongoDbContext>>();
        return services;
    }

    public static IServiceCollection AddMongoDBUserTokenRepository<TMongoDbContext>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
    {
        services.TryAddScoped<IUserTokenRepository, MongoUserTokenRepository<TMongoDbContext>>();
        return services;
    }
}