using uBeac;
using uBeac.FileManagement;
using uBeac.FileManagement.MongoDB;
using uBeac.Repositories.MongoDB;

namespace Microsoft.Extensions.DependencyInjection;

public static class FileServiceBuilderExtensions
{
    public static IFileServiceBuilder<TKey, TEntity> StoreInfoInMongoDb<TKey, TEntity, TContext>(this IFileServiceBuilder<TKey, TEntity> builder)
        where TKey : IEquatable<TKey>
        where TEntity : IFileEntity<TKey>
        where TContext : IMongoDBContext
    {
        builder.Repository = ActivatorUtilities.GetServiceOrCreateInstance<MongoFileRepository<TKey, TEntity, TContext>>;
        return builder;
    }

    public static IFileServiceBuilder<Guid, TEntity> StoreInfoInMongoDb<TEntity, TContext>(this IFileServiceBuilder<Guid, TEntity> builder)
        where TEntity : IFileEntity
        where TContext : IMongoDBContext
        => StoreInfoInMongoDb<Guid, TEntity, TContext>(builder);

    public static IFileServiceBuilder<Guid, FileEntity> StoreInfoInMongoDb<TContext>(this IFileServiceBuilder<Guid, FileEntity> builder)
        where TContext : IMongoDBContext
        => StoreInfoInMongoDb<Guid, FileEntity, TContext>(builder);

    public static IFileServiceBuilder<Guid, FileEntity> StoreInfoInMongoDb(this IFileServiceBuilder<Guid, FileEntity> builder)
        => StoreInfoInMongoDb<Guid, FileEntity, MongoDBContext>(builder);
}