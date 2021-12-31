namespace uBeac.Identity
{
    public class TokenResult<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey UserId { get; set; }
        public string Token { get; }
        public string RefreshToken { get; }
        public DateTime Expiry { get; }

        public TokenResult(TUserKey userId, string token, string refreshToken, DateTime expiry)
        {
            RefreshToken = refreshToken;
            Token = token;
            Expiry = expiry;
            UserId = userId;
        }
    }
    public class TokenResult : TokenResult<Guid>
    {
        public TokenResult(Guid userId, string token, string refreshToken, DateTime expirey) : base(userId, token, refreshToken, expirey)
        {
        }
    }

}
