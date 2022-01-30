using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class RoleAddRequest
{
    [Required]
    public virtual string Name { get; set; }
}

public class RoleUpdateRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public virtual TKey Id { get; set; }

    [Required]
    public virtual string Name { get; set; }
}
public class RoleUpdateRequest : RoleUpdateRequest<Guid>
{
}

public class RoleResponse<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    public virtual string Name { get; set; }
}

public class RoleResponse : RoleResponse<Guid>
{
}

