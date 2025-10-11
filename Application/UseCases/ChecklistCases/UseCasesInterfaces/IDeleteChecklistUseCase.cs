using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;

public interface IDeleteChecklistUseCase
{
  Task Execute(long listId);
}