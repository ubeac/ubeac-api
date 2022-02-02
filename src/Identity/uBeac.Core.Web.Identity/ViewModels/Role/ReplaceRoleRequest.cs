using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ReplaceRoleRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public virtual TKey Id { get; set; }

    [Required]
    public virtual string Name { get; set; }
}

public class ReplaceRoleRequest : ReplaceRoleRequest<Guid>
{
}

