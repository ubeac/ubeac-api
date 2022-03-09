namespace uBeac.Identity;

public class TokenResult<TUserKey> where TUserKey : IEquatable<TUserKey>
{
    public virtual TUserKey UserId { get; set; }
    public virtual List<string> Roles { get; set; }
    public virtual string Token { get; set; }
    public virtual string RefreshToken { get; set; }
}

public class TokenResult : TokenResult<Guid>
{
}