namespace uBeac.Web;
public class IdRequest<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}

public class IdRequest : IdRequest<Guid>
{
}