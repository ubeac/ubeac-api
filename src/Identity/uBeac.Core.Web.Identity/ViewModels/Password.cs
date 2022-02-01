using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity;

public class ResetPasswordRequest
{
    [Required]
    [EmailAddress]
    public virtual string Email { get; set; }
}

public class ChangePasswordRequest<TKey> where TKey : IEquatable<TKey>
{
    [Required]
    public TKey UserId { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public virtual string CurrentPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public virtual string NewPassword { get; set; }
}

public class ChangePasswordRequest : ChangePasswordRequest<Guid>
{
}

public class ForgotPasswordRequest
{
    [Required]
    public virtual string Username { get; set; }
}