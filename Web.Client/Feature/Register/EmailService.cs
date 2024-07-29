using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Web.Client.Interfaces;
using Web.Client.Shared.Models;

namespace Web.Client.Feature.Register;

public class EmailService : IMailService
{
    private readonly MailSettings Mail_Settings;

    public EmailService(IOptions<MailSettings> options)
    {
        Mail_Settings = options.Value;
    }

    public bool SendMail(MimeMessage email)
    {
        try
        {
            using var MailClient = new SmtpClient();
            MailClient.CheckCertificateRevocation = false;
            MailClient.Connect(Mail_Settings.Host, Mail_Settings.Port, Mail_Settings.UseSSL);
            MailClient.Authenticate(Mail_Settings.EmailId, Mail_Settings.Password);
            MailClient.Send(email);
            MailClient.Disconnect(true);
            return true;
        }
        catch (Exception ex)
        {
            // Exception Details
            return false;
        }
    }

    public async Task<bool> SendMailAsync(MimeMessage email, CancellationToken ct)
    {
        try
        {
            using var MailClient = new SmtpClient();
            MailClient.CheckCertificateRevocation = false;
            await MailClient.ConnectAsync(Mail_Settings.Host, Mail_Settings.Port, Mail_Settings.UseSSL, ct);
            await MailClient.AuthenticateAsync(Mail_Settings.EmailId, Mail_Settings.Password, ct);
            await MailClient.SendAsync(email, ct);
            await MailClient.DisconnectAsync(true, ct);
            return true;
        }
        catch
        {
            // Exception Details
            return false;
        }
    }
}