namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IVerifyValidAssignmentService
{
  Task<bool> Execute(string userId, string title, long subjectId);
}