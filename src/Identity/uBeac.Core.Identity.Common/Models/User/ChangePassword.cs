namespace uBeac.Identity;

public class ChangePassword<TKey> where TKey : IEquatable<TKey>
{
    public TKey? UserId { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}

public class ChangePassword : ChangePassword<Guid>
{
}
