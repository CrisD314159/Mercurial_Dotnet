using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;

public interface IDeleteAssignmentUseCase
{
  Task Execute(string userId, Guid assignmentId);
}