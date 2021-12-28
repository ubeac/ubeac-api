namespace uBeac.Identity
{
    public class TokenResult<TUserKey, TUser> 
        where TUserKey : IEquatable<TUserKey> 
        where TUser : User<TUserKey>
    {
        public string Token { get; }
        public DateTime Expires { get; }
        public TUser User { get; set; }

        public TokenResult(TUser user, string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
            User = user;
        }
    }
}
