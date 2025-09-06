using System.Net;
using System.Net.Mail;
using BLL.Services;
using Microsoft.Extensions.Configuration;

public class EmailSender : IEmailSender
{
    private readonly string _fromEmail;
    private readonly string _fromName;
    private readonly string _password;

    public EmailSender(IConfiguration configuration)
    {
        // Read credentials from appsettings.json
        _fromEmail = configuration["EmailSettings:Email"];
        _fromName = configuration["EmailSettings:Name"];
        _password = configuration["EmailSettings:Password"];
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress(_fromEmail, _fromName);
        mail.To.Add(toEmail);
        mail.Subject = subject;
        mail.Body = htmlMessage;
        mail.IsBodyHtml = true;

        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(_fromEmail, _password),
            EnableSsl = true
        };

        await smtp.SendMailAsync(mail);
    }
}
