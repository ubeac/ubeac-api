using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public class User<TUserKey> : IdentityUser<TUserKey>, IEntity<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public virtual string AuthenticatorKey { get; set; }
        public virtual List<IdentityUserClaim<TUserKey>> Claims { get; }
        public virtual List<IdentityUserLogin<TUserKey>> Logins { get; }
        public virtual List<IdentityUserToken<TUserKey>> Tokens { get; }
        public virtual List<TwoFactorRecoveryCode> RecoveryCodes { get; }
        public virtual IList<string> Roles { get; }

        public User()
        {
            Claims = new List<IdentityUserClaim<TUserKey>>();
            Logins = new List<IdentityUserLogin<TUserKey>>();
            Tokens = new List<IdentityUserToken<TUserKey>>();
            RecoveryCodes = new List<TwoFactorRecoveryCode>();
            Roles = new List<string>();
            AuthenticatorKey = string.Empty;
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }
    }
}
