using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitExtensions
{
    public static IServiceCollection AddMongoDBUnitRepository<TMongoDbContext, TUnitKey, TUnit>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        services.TryAddScoped<IUnitRepository<TUnitKey, TUnit>, MongoUnitRepository<TUnitKey, TUnit, TMongoDbContext>>();
        return services;
    }

    public static IServiceCollection AddMongoDBUnitRepository<TMongoDbContext, TUnit>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUnit : Unit
    {
        services.TryAddScoped<IUnitRepository<TUnit>, MongoUnitRepository<TUnit, TMongoDbContext>>();
        return services;
    }
}