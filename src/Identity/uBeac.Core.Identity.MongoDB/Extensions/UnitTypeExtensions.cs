using Microsoft.Extensions.DependencyInjection.Extensions;
using uBeac.Identity;
using uBeac.Identity.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class UnitTypeExtensions
{
    public static IServiceCollection AddMongoDBUnitTypeRepository<TMongoDbContext, TUnitTypeKey, TUnitType>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUnitTypeKey : IEquatable<TUnitTypeKey>
        where TUnitType : UnitType<TUnitTypeKey>
    {
        services.TryAddScoped<IUnitTypeRepository<TUnitTypeKey, TUnitType>>(provider =>
        {
            var dbContext = provider.GetRequiredService<TMongoDbContext>();
            return new MongoUnitTypeRepository<TUnitTypeKey, TUnitType>(dbContext);
        });

        return services;
    }

    public static IServiceCollection AddMongoDBUnitTypeRepository<TMongoDbContext, TUnitType>(this IServiceCollection services)
        where TMongoDbContext : class, IMongoDBContext
        where TUnitType : UnitType
    {
        services.TryAddScoped<IUnitTypeRepository<TUnitType>>(provider =>
        {
            var dbContext = provider.GetRequiredService<TMongoDbContext>();
            return new MongoUnitTypeRepository<TUnitType>(dbContext);
        });

        return services;
    }
}