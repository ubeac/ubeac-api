using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class RoleExtensions
{
    public static IServiceCollection AddMongoDBRoleRepository<TMongoDbContext, TRoleKey, TRole>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TRoleKey : IEquatable<TRoleKey>
        where TRole : Role<TRoleKey>
    {
        services.TryAddScoped<IRoleRepository<TRoleKey, TRole>, MongoRoleRepository<TRoleKey, TRole, TMongoDbContext>>();
        return services;
    }

    public static IServiceCollection AddMongoDBRoleRepository<TMongoDbContext, TRole>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TRole : Role
    {
        services.TryAddScoped<IRoleRepository<TRole>, MongoRoleRepository<TRole, TMongoDbContext>>();
        return services;
    }
}