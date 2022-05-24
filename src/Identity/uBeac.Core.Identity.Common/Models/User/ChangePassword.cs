namespace uBeac.Identity;

public class ChangePassword<TKey> where TKey : IEquatable<TKey>
{
    public ChangePassword() { }

    public ChangePassword(TKey userId, string currentPassword, string newPassword)
    {
        UserId = userId;
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }

    public TKey UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePassword : ChangePassword<Guid>
{
    public ChangePassword(Guid userId, string currentPassword, string newPassword) : base(userId, currentPassword, newPassword)
    {
    }
}
