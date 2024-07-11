using MimeKit;

namespace Web.Client.Interfaces;

public interface IMailService
{
    bool SendMail(MimeMessage email);
    Task<bool> SendMailAsync(MimeMessage email);
}