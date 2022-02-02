using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class InsertUnitRequest
{
    [Required]
    public virtual string Name { get; set; }

    [Required]
    public virtual string Code { get; set; }

    [Required]
    public virtual string Type { get; set; }

    public virtual string Description { get; set; }
    public virtual string ParentUnitId { get; set; }
}