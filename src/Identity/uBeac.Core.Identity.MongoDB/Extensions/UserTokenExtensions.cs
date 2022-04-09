using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserTokenExtensions
{
    public static IServiceCollection AddMongoDBUserTokenRepository<TMongoDbContext, TUserKey>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUserKey : IEquatable<TUserKey>
    {
        services.TryAddScoped<IUserTokenRepository<TUserKey>, MongoUserTokenRepository<TUserKey, TMongoDbContext>>();
        services.TryAddScoped<IEntityHistoryRepository<TUserKey, UserToken<TUserKey>>, MongoEntityHistoryRepository<TUserKey, UserToken<TUserKey>, TMongoDbContext>>();
        return services;
    }

    public static IServiceCollection AddMongoDBUserTokenRepository<TMongoDbContext>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
    {
        services.TryAddScoped<IUserTokenRepository, MongoUserTokenRepository<TMongoDbContext>>();
        services.TryAddScoped<IEntityHistoryRepository<Guid, UserToken<Guid>>, MongoEntityHistoryRepository<Guid, UserToken<Guid>, TMongoDbContext>>();
        return services;
    }
}