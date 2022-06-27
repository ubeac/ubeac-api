using uBeac;
using uBeac.FileManagement;
using uBeac.FileManagement.LocalStorage;

namespace Microsoft.Extensions.DependencyInjection;

public static class FileServiceBuilderExtensions
{
    public static IFileServiceBuilder<TKey, TEntity> StoreFilesInLocalStorage<TKey, TEntity>(this IFileServiceBuilder<TKey, TEntity> builder, FileManagementLocalStorageOptions options)
        where TKey : IEquatable<TKey>
        where TEntity : IFileEntity<TKey>
    {
        builder.Provider = serviceProvider => ActivatorUtilities.CreateInstance<LocalStorageFileProvider>(serviceProvider, options);
        return builder;
    }
}