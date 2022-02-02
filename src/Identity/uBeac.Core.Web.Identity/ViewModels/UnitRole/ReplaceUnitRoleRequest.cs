using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ReplaceUnitRoleRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public virtual TKey Id { get; set; }

    [Required]
    public virtual string UserName { get; set; }

    [Required]
    public virtual string UnitCode { get; set; }

    [Required]
    public virtual string UnitType { get; set; }

    [Required]
    public virtual string Role { get; set; }
}

public class ReplaceUnitRoleRequest : ReplaceUnitRequest<Guid>
{
}