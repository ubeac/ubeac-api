namespace uBeac.Identity
{
    public class TokenResult<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class TokenResult : TokenResult<Guid>
    {
    }

}
