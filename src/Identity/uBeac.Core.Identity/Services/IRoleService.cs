namespace uBeac.Identity
{
    public interface IRoleService<TKey, TRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey>
    {
        Task Insert(TRole role, CancellationToken cancellationToken = default);
        Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
        Task<bool> Update(TRole role, CancellationToken cancellationToken = default);
        Task<IEnumerable<TRole>> GetAll(CancellationToken cancellationToken = default);
    }

    public interface IRoleService<TRole>: IRoleService<Guid, TRole>
       where TRole : Role
    { 
    }
}
