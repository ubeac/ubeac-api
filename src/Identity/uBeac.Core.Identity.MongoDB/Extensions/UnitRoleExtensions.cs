using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitRoleExtensions
{
    public static IServiceCollection AddMongoDBUnitRoleRepository<TMongoDbContext, TUnitRoleKey, TUnitRole>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUnitRoleKey : IEquatable<TUnitRoleKey>
        where TUnitRole : UnitRole<TUnitRoleKey>
    {
        services.TryAddScoped<IUnitRoleRepository<TUnitRoleKey, TUnitRole>, MongoUnitRoleRepository<TUnitRoleKey, TUnitRole, TMongoDbContext>>();
        return services;
    }

    public static IServiceCollection AddMongoDBUnitRoleRepository<TMongoDbContext, TUnitRole>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUnitRole : UnitRole
    {
        services.TryAddScoped<IUnitRoleRepository<TUnitRole>, MongoUnitRoleRepository<TUnitRole, TMongoDbContext>>();
        return services;
    }
}