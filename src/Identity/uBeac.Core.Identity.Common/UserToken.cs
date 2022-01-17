namespace uBeac.Identity
{
    public class UserToken<TUserKey>: Entity<TUserKey> 
        where TUserKey : IEquatable<TUserKey>
    {
        public List<string> Tokens { get; set; } = new();
    }
}
