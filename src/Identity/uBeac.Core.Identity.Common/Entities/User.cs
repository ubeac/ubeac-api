using Microsoft.AspNetCore.Identity;

namespace uBeac.Identity;

public class User<TUserKey> : IdentityUser<TUserKey>, IEntity<TUserKey> where TUserKey : IEquatable<TUserKey>
{
    public virtual DateTime? LastLoginAt { get; set; }
    public virtual int LoginsCount { get; set; }
    public virtual DateTime? LastPasswordChangedAt { get; set; }
    public virtual string LastPasswordChangedBy { get; set; }
    public virtual bool Enabled { get; set; } = false;

    public virtual string AuthenticatorKey { get; set; }
    public virtual List<IdentityUserClaim<TUserKey>> Claims { get; set; } = new();
    public virtual List<IdentityUserLogin<TUserKey>> Logins { get; set; } = new();
    public virtual List<IdentityUserToken<TUserKey>> Tokens { get; set; } = new();
    public virtual List<TwoFactorRecoveryCode> RecoveryCodes { get; set; } = new();
    public virtual List<string> Roles { get; set; } = new();

    public User()
    {
    }

    public User(string userName) : base(userName)
    {
    }
}

public class User : User<Guid>, IEntity
{
    public User()
    {
    }

    public User(string userName) : base(userName)
    {
    }
}