using uBeac;
using uBeac.Repositories;
using uBeac.Repositories.History;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection EnableHistory<TEntityKey, TEntity, TRepositoryInterface, TRepositoryImplementation>(this IServiceCollection services)
        where TEntityKey : IEquatable<TEntityKey>
        where TEntity : IEntity<TEntityKey>
        where TRepositoryInterface : IEntityRepository<TEntityKey, TEntity>
        where TRepositoryImplementation : class, TRepositoryInterface
    {
        services.AddDecorator<TRepositoryInterface, TRepositoryImplementation, EntityRepositoryHistoryDecorator<TEntityKey, TEntity>>();
        return services;
    }

    public static IServiceCollection EnableHistory<TEntity, TRepositoryInterface, TRepositoryImplementation>(this IServiceCollection services)
        where TEntity : IEntity
        where TRepositoryInterface : IEntityRepository<TEntity>
        where TRepositoryImplementation : class, TRepositoryInterface
    {
        services.AddDecorator<TRepositoryInterface, TRepositoryImplementation, EntityRepositoryHistoryDecorator<TEntity>>();
        return services;
    }
}