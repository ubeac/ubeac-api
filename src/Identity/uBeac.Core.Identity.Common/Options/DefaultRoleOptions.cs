namespace uBeac.Identity;

public class DefaultRoleOptions<TKey, TRole> : DefaultOptions<TKey, TRole>
    where TKey : IEquatable<TKey>
    where TRole : Role<TKey>
{
    public TRole AdminRole { get; set; }
}

public class DefaultRoleOptions<TRole> : DefaultOptions<Guid, TRole>
    where TRole : Role
{
    public TRole AdminRole { get; set; }
}
