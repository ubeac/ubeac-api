using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace uBeac;

public interface IEmailProvider
{
    Task Send(string receptor, string subject, string body, bool isBodyHtml = false);
}

public class EmailProvider : IEmailProvider
{
    protected readonly EmailProviderOptions Options;
    protected readonly ILogger<EmailProvider> Logger;

    public EmailProvider(IOptions<EmailProviderOptions> options, ILogger<EmailProvider> logger)
    {
        Options = options.Value;
        Logger = logger;
    }

    // todo: to, cc, bcc: array string
    public virtual async Task Send(string receptor, string subject, string body, bool isBodyHtml = false)
    {
        try
        {
            var smtpClient = new SmtpClient(Options.SmtpHost, Options.SmtpPort)
            {
                Credentials = new NetworkCredential(Options.UserName, Options.Password),
                EnableSsl = Options.EnableSsl
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(Options.MailAddress, Options.DisplayName, Encoding.UTF8),
                To = { new MailAddress(receptor) },
                Subject = subject,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = isBodyHtml
            };
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception e)
        {
            Logger.LogError(e, e.Message);
        }
    }
}