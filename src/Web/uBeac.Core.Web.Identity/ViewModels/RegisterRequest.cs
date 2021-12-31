using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity
{
    /// <summary>
    /// User registration request model
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
