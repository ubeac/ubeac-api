namespace uBeac.Identity;

public class ReplaceRole<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public string Name { get; set; }
}

public class ReplaceRole : ReplaceRole<Guid>
{
}