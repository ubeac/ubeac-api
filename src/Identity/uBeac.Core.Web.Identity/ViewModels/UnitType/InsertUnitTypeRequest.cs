using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class InsertUnitTypeRequest
{
    [Required]
    public virtual string Code { get; set; }

    [Required]
    public virtual string Name { get; set; }

    public virtual string Description { get; set; }
}