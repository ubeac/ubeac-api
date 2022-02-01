namespace uBeac.Web.Identity;

public class UserResponse<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual bool PhoneNumberConfirmed { get; set; }
}

public class ReplaceUserRequest<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual bool PhoneNumberConfirmed { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
    public virtual bool LockoutEnabled { get; set; }
    public virtual DateTimeOffset? LockoutEnd { get; set; }
}

public class ReplaceUserRequest : ReplaceUserRequest<Guid>
{
}

public class InsertUserRequest
{
    public virtual string UserName { get; set; }
    public virtual string Password { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual bool PhoneNumberConfirmed { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
}