namespace uBeac.Web.Identity
{
    public class RegisterResponse<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        public TUserKey Id { get; set; } = default;
        public virtual string Username { get; set; } = string.Empty;
        public virtual string Email { get; set; } = string.Empty;
        public string Token { get; } = string.Empty;
        public DateTime Expires { get; } = DateTime.Now;
    }
}
