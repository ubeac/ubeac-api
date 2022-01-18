using uBeac.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection InsertDefaultUnits<TUnitKey, TUnit>(this IServiceCollection services, List<TUnit> defaultUnits) 
        where TUnitKey : IEquatable<TUnitKey>
        where TUnit : Unit<TUnitKey>
    {
        var repository = services.BuildServiceProvider().GetService<IUnitRepository<TUnitKey, TUnit>>();

        if (repository == null)
            throw new NullReferenceException("Repository is not registered for unit");

        repository.InsertMany(defaultUnits);
        return services;
    }

    public static IServiceCollection InsertDefaultUnits<TUnit>(this IServiceCollection services, List<TUnit> defaultUnits)
        where TUnit : Unit
    {
        var repository = services.BuildServiceProvider().GetService<IUnitRepository<TUnit>>();

        if (repository == null)
            throw new NullReferenceException("Repository is not registered for unit");

        repository.InsertMany(defaultUnits);
        return services;
    }
}