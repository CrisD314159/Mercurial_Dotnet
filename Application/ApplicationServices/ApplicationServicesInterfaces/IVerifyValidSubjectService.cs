namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IVerifyValidSubjectService
{
  Task<bool> Execute(string userId, string title);
}