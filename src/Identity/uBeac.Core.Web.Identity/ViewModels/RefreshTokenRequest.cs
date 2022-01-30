using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;
public class RefreshTokenRequest
{
    [Required]
    public virtual string Token { get; set; } = string.Empty;

    [Required]
    public virtual string RefreshToken { get; set; } = string.Empty;
}
