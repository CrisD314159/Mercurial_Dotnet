using MimeKit;

namespace MercurialBackendDotnet.Application.ExternalServices;

public interface IEmailService
{
  Task ReadFileToSendEmail(string name, string subject, string email, string content, string? verificationCode, string file, IConfiguration configuration);

  Task SendEmail(MimeMessage message, IConfiguration configuration);
}