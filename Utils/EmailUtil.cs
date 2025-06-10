using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using Resend;

namespace MercurialBackendDotnet.Utils;

public static class EmailUtil
{

  public static async Task ReadFileToSendEmail(string name, string subject, string email, string content, string? verificationCode, string file)
  {
    var html = File.ReadAllText($"./Templates/{file}.html");
    html = html.Replace("{{name}}", name);
    html = html.Replace("{{content}}", content);
    if(!string.IsNullOrEmpty(verificationCode)) html = html.Replace("{{verification_code}}", verificationCode);
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("Mercurial", "mercurial.app24@gmail.com"));
    message.To.Add(new MailboxAddress(name, email));
    message.Subject = subject;
    message.Body = new TextPart("html"){Text =html};
    await SendSMTPMessage(message);
  }


  public static async Task SendSMTPMessage(MimeMessage message)
  {
    var client = new SmtpClient();
    var key = DotNetEnv.Env.GetString("GOOGLE_KEY");
    var gmail = DotNetEnv.Env.GetString("GOOGLE_MAIL");

    await client.ConnectAsync("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);

    await client.AuthenticateAsync(gmail, key);

    await client.SendAsync(message);

  }

  public static async Task SendResendEmail(string subject, string email, string html)
  {
    var key = DotNetEnv.Env.GetString("RESEND_KEY");
    IResend resend = ResendClient.Create(key);
    var resp =await resend.EmailSendAsync(new EmailMessage()
    {
      From = "Mercurial <noreply@resend.dev>",
      To = email,
      Subject = subject,
      HtmlBody = html,
    });
    Console.WriteLine(resp.Content);
  }
  
}