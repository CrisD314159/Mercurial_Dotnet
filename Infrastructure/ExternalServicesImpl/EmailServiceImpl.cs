using MailKit.Net.Smtp;
using MercurialBackendDotnet.Application.ExternalServices;
using MimeKit;

namespace MercurialBackendDotnet.Infrastructure.ExternalServicesImpl;

public class EmailServiceImpl(IConfiguration configuration): IEmailService
{
    private readonly IConfiguration _configuration = configuration;

    public async Task ReadFileToSendEmail(string name, string subject, string email, string content, string? verificationCode,
        string file)
    {
        var html = await File.ReadAllTextAsync($"./Presentation/Templates/{file}.html");
        html = html.Replace("{{name}}", name);
        html = html.Replace("{{content}}", content);
        if(!string.IsNullOrEmpty(verificationCode)) html = html.Replace("{{verification_code}}", verificationCode);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Mercurial", "mercurial.app24@gmail.com"));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = subject;
        message.Body = new TextPart("html"){Text =html};
        await SendEmail(message);
    }

    public async Task SendEmail(MimeMessage message)
    {
        var client = new SmtpClient();
        var key = _configuration["Gmail:Token"];
        var gmail = _configuration["Gmail:Mail"];

        await client.ConnectAsync("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);

        await client.AuthenticateAsync(gmail, key);

        await client.SendAsync(message);
    }

    public async Task SendAccountCreatedVerificationCode(string userName, string userEmail, string userVerificationCode)
    {
        await ReadFileToSendEmail(userName,
             "Welcome to Mercurial",
             userEmail,
             "Use the following code to verify your account",
             userVerificationCode,
             "emailTemplate"
             );
    }

    public async Task SendRecoverAccountVerificationCode(string userName, string userEmail, string userVerificationCode)
    {
        await ReadFileToSendEmail(userName,
             "Recover your account",
             userEmail,
             "Click the following button to recover your account",
             userVerificationCode,
             "recoveryEmailTemplate"
             );
    }
}

