namespace uBeac.FileManagement;

public interface IFileManagementBuilder<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IFileEntity<TKey>
{
    IFileServiceBuilder<TKey, TEntity> AddCategory(string categoryName);
}