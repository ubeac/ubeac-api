using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class RegisterRequest
{
    [Required]
    public virtual string Username { get; set; }

    [Required]
    [EmailAddress]
    public virtual string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public virtual string Password { get; set; }
}