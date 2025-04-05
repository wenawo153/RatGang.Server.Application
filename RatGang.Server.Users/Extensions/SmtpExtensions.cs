using System.Net.Mail;

namespace RatGang.Server.Users.Extensions;

public static class SmtpExtensions
{
    public static async Task SendAsync(SendSmtpOptions options)
    {
        MailAddress sender = new(
            Configurate.Singleton.SmtpEmailOptions.EmailOptions.Address,
            Configurate.Singleton.SmtpEmailOptions.EmailOptions.DisplayName);

        MailAddress recipient = new(options.RecipientEmail);

        MailMessage message = new(sender, recipient)
        {
            Body = options.Body,
            Subject = options.Subject,
        };

        SmtpClient smtpClient = new(
            host: Configurate.Singleton.SmtpEmailOptions.SmtpOptions.Host,
            port: int.Parse(Configurate.Singleton.SmtpEmailOptions.SmtpOptions.Port))
        {
            Credentials = new System.Net.NetworkCredential(
                Configurate.Singleton.SmtpEmailOptions.NetworkCredential.UserName,
                Configurate.Singleton.SmtpEmailOptions.NetworkCredential.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);
    }
}

public class SendSmtpOptions
{
    public string Body { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
}
