using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ResetPasswordRequest
{
    [Required]
    [EmailAddress]
    public virtual string Email { get; set; }
}