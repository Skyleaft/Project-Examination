using MimeKit;

namespace Web.Client.Interfaces;

public interface IMailService
{
    bool SendMail(MimeMessage email);
}
