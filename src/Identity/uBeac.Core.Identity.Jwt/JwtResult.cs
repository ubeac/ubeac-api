namespace uBeac.Identity;

public class JwtResult
{
    public virtual string Token { get; set; } = string.Empty;
    public virtual DateTime Expiry { get; set; }
}