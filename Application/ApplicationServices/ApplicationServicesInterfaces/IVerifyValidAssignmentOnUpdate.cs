namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;


public interface IVerifyValidAssignmentOnUpdate
{
    Task<bool> Execute(Guid assignmentId, string userId, string title, long subjectId);
}