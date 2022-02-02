namespace uBeac.Web.Identity;

public class RoleViewModel<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string Name { get; set; }
}

public class RoleViewModel : RoleViewModel<Guid>
{
}