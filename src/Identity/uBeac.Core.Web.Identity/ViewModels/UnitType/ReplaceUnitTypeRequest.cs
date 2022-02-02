using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ReplaceUnitTypeRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public virtual TKey Id { get; set; }

    [Required]
    public virtual string Code { get; set; }

    [Required]
    public virtual string Name { get; set; }

    public virtual string Description { get; set; }
}

public class ReplaceUnitTypeRequest : ReplaceUnitRequest<Guid>
{
}