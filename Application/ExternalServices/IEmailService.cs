using MimeKit;

namespace MercurialBackendDotnet.Application.ExternalServices;

public interface IEmailService
{
  Task ReadFileToSendEmail(string name, string subject, string email, string content, string? verificationCode, string file);
  Task SendEmail(MimeMessage message);
  Task SendAccountCreatedVerificationCode(string userName, string userEmail, string userVerificationCode);
  Task SendRecoverAccountVerificationCode(string userName, string userEmail, string userVerificationCode);
}