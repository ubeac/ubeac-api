using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ForgotPasswordRequest
{
    [Required]
    public virtual string Username { get; set; }
}