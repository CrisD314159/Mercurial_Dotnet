namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IGenerateSessionService
{
  Task<string> CreateSession(string userId, string email);
}