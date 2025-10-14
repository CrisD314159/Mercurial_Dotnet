using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;

public interface IUpdateAssignmentUseCase
{
  Task Execute(string userId, UpdateAssignmentDTO updateAssignmentDTO);
}