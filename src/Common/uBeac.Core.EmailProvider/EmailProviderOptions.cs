namespace uBeac;

public class EmailProviderOptions
{
    public virtual string DisplayName { get; set; }
    public virtual string MailAddress { get; set; }
    public virtual string SmtpHost { get; set; }
    public virtual int SmtpPort { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Password { get; set; }
}