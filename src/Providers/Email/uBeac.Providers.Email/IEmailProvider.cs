namespace uBeac.Providers.Email;

public interface IEmailProvider
{
    Task Send(string to, string cc, string bcc, string subject, string body, CancellationToken cancellationToken = default);
}