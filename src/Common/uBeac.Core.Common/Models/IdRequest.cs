using System.ComponentModel.DataAnnotations;

namespace uBeac;

public class IdRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public virtual TKey? Id { get; set; }
}

public class IdRequest : IdRequest<Guid>
{
}