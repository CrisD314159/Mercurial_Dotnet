using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;

public interface ICreateChecklistUseCase
{
  Task Execute(Guid assignmentId);
}