namespace uBeac.Web.Identity;

public class UserResponse<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string Username { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual bool PhoneNumberConfirmed { get; set; }
}
