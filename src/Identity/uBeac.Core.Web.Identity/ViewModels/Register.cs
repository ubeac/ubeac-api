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

public class RegisterResponse<TUserKey> where TUserKey : IEquatable<TUserKey>
{
    public virtual TUserKey Id { get; set; }
    public virtual string Username { get; set; }
    public virtual string Email { get; set; }
    public virtual string Token { get; }
    public virtual DateTime Expires { get; }
}