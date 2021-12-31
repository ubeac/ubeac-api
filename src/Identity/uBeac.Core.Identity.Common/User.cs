using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity
{
    public class User<TUserKey> : IdentityUser<TUserKey>, IEntity<TUserKey>
        where TUserKey : IEquatable<TUserKey>
    {
        public virtual string AuthenticatorKey { get; set; } = string.Empty;
        public virtual List<IdentityUserClaim<TUserKey>> Claims { get; } = new();
        public virtual List<IdentityUserLogin<TUserKey>> Logins { get; } = new();
        public virtual List<IdentityUserToken<TUserKey>> Tokens { get; } = new();
        public virtual List<TwoFactorRecoveryCode> RecoveryCodes { get; } = new();
        public virtual List<string> Roles { get; } = new();

        public User()
        {
        }

        public User(string userName)
        {
            UserName = userName;
        }
    }

    public class User : User<Guid>, IEntity
    {
    }
}
