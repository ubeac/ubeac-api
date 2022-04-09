using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserExtensions
{
    public static IServiceCollection AddMongoDBUserRepository<TMongoDbContext, TUserKey, TUser>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUserKey : IEquatable<TUserKey>
        where TUser : User<TUserKey>
    {
        services.TryAddScoped<IUserRepository<TUserKey, TUser>, MongoUserRepository<TUserKey, TUser, TMongoDbContext>>();
        services.TryAddScoped<IEntityHistoryRepository<TUserKey, TUser>, MongoEntityHistoryRepository<TUserKey, TUser, TMongoDbContext>>();
        return services;
    }

    public static IServiceCollection AddMongoDBUserRepository<TMongoDbContext, TUser>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUser : User
    {
        services.TryAddScoped<IUserRepository<TUser>, MongoUserRepository<TUser, TMongoDbContext>>();
        services.TryAddScoped<IEntityHistoryRepository<TUser>, MongoEntityHistoryRepository<TUser, TMongoDbContext>>();
        return services;
    }
}