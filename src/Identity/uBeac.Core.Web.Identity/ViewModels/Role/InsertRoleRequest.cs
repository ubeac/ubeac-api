using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class InsertRoleRequest
{
    [Required]
    public virtual string Name { get; set; }
}