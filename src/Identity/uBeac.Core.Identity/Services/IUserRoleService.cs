//namespace uBeac.Identity
//{
//    public interface IUserRoleService<TKey, TUser, TRole>
//        where TKey : IEquatable<TKey>
//        where TUser : User<TKey>
//        where TRole : Role<TKey>
//    {
//        Task<bool> AddRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default);
//        Task<bool> RemoveRoles(TKey userId, IEnumerable<TKey> roleIds, CancellationToken cancellationToken = default);
//        Task<IEnumerable<TRole>> GetRolesForUser(TKey userId, CancellationToken cancellationToken = default);
//        Task<IEnumerable<TUser>> GetUsersInRole(TKey roleId, CancellationToken cancellationToken = default);
//    }
//}
