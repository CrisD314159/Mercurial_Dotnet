using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;

public interface IMarkAssignmentAsDoneUseCase
{
  Task Execute(string userId, Guid assignmentId);
}