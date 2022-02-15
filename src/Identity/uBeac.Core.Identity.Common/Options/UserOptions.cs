namespace uBeac.Identity;

public class UserOptions<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : User<TKey>
{
    public TUser AdminUser { get; set; }
    public string AdminPassword { get; set; }
    public string AdminRole { get; set; }
}

public class UserOptions<TUser> : UserOptions<Guid, TUser>
    where TUser : User
{
}