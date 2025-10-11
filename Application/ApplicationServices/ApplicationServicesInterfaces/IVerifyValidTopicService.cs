namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IVerifyValidTopicService
{
  Task<bool> Execute(string title, string userId);
}