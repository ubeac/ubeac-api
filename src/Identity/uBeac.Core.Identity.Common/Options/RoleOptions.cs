namespace uBeac.Identity;

public class RoleOptions<TKey, TRole> : IOptions<TKey, TRole>
    where TKey : IEquatable<TKey>
    where TRole : Role<TKey>
{
    public IEnumerable<TRole>? DefaultValues { get; set; }
}

public class RoleOptions<TRole> : RoleOptions<Guid, TRole>, IOptions<TRole>
    where TRole : Role
{
}
