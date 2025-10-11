namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IValidateAndGenerateValidUserService
{
  Task<string> Execute(string currentUsername);
}