using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace uBeac;

public interface IEmailProvider
{
    Task Send(string recipients, string subject, string body, string ccs = null, string bccs = null, CancellationToken cancellationToken = default);
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

    public virtual async Task Send(string recipients, string subject, string body, string ccs = null, string bccs = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(recipients))
            throw new ArgumentNullException(nameof(recipients));

        var smtpClient = new SmtpClient(Options.SmtpHost, Options.SmtpPort)
        {
            Credentials = new NetworkCredential(Options.UserName, Options.Password),
            EnableSsl = Options.EnableSsl
        };
        var mailMessage = new MailMessage
        {
            From = new MailAddress(Options.MailAddress, Options.DisplayName, Encoding.UTF8),
            Subject = subject,
            Body = body,
            BodyEncoding = Encoding.UTF8,
            IsBodyHtml = true
        };

        mailMessage.To.Add(recipients);

        if (!string.IsNullOrEmpty(ccs))
            mailMessage.CC.Add(ccs);

        if (!string.IsNullOrEmpty(bccs))
            mailMessage.Bcc.Add(bccs);

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);

    }
}