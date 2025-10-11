using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;

public interface ICreateAssignmentUseCase
{
  Task Execute(string userId, CreateAssignmentDTO createAssignmentDTO);
}