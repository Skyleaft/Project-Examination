using MimeKit;

namespace Web.Client.Interfaces;

public interface IMailService
{
    void SendMail(MimeMessage email);
    Task SendMailAsync(string toEmail, string subject, string body, CancellationToken ct);
    void SendMailBackground(string toEmail, string subject, string body, CancellationToken ct);
}