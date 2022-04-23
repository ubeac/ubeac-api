using System.Net;
using System.Net.Mail;

namespace uBeac.Providers.Email;

public class EmailProvider : IEmailProvider
{
    protected readonly SmtpSettings SmtpSettings;
    protected readonly SmtpClient SmtpClient;

    public EmailProvider(SmtpSettings smtpSettings)
    {
        SmtpSettings = smtpSettings;
        SmtpClient = new SmtpClient(SmtpSettings.Server, SmtpSettings.Port)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(SmtpSettings.Username, SmtpSettings.Password),
            EnableSsl = SmtpSettings.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
    }

    public async Task Send(string to, string cc, string bcc, string subject, string body, CancellationToken cancellationToken = default)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(SmtpSettings.SenderEmail, SmtpSettings.SenderName),
            Body = body,
            IsBodyHtml = true,
            Subject = subject,
            BodyEncoding = System.Text.Encoding.UTF8,
            SubjectEncoding = System.Text.Encoding.UTF8,
        };
        mailMessage.To.Add(to);

        if (!string.IsNullOrEmpty(cc))
        {
            var ccArray = SplitString(cc);
            foreach (var ccStr in ccArray) mailMessage.CC.Add(ccStr);
        }

        if (!string.IsNullOrEmpty(bcc))
        {
            var bccArray = SplitString(bcc);
            foreach (var bccStr in bccArray) mailMessage.Bcc.Add(bccStr);
        }

        await SmtpClient.SendMailAsync(mailMessage, cancellationToken);
    }

    protected virtual string[] SplitString(string str) => str.Split(",", StringSplitOptions.RemoveEmptyEntries);
}