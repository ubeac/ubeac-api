using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ReplaceUnitRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public virtual TKey Id { get; set; }

    [Required]
    public virtual string Name { get; set; }

    [Required]
    public virtual string Code { get; set; }

    [Required]
    public virtual string Type { get; set; }

    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}

public class ReplaceUnitRequest : ReplaceUnitRequest<Guid>
{
}