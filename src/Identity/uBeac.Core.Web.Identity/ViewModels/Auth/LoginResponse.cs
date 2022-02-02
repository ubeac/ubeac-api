namespace uBeac.Web.Identity;

public class LoginResponse<TUserKey>
    where TUserKey : IEquatable<TUserKey>
{
    public virtual TUserKey Id { get; set; }
    public virtual string Username { get; set; }
    public virtual string Email { get; set; }
    public virtual string Token { get; set; }
    public virtual string RefreshToken { get; set; }
    public virtual DateTime Expiry { get; set; }
}

public class LoginResponse : LoginResponse<Guid>
{
}