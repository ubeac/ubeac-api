namespace uBeac.Identity;

public class TokenResult<TUserKey> where TUserKey : IEquatable<TUserKey>
{
    public TUserKey UserId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class TokenResult : TokenResult<Guid>
{
}