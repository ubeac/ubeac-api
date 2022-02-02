using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class LoginRequest
{
    [Required]
    public virtual string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public virtual string Password { get; set; }
}