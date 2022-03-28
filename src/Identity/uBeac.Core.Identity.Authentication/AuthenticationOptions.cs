namespace uBeac.Identity;

public class AuthenticationOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}