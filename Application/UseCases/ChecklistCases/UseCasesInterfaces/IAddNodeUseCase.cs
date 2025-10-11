using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;

public interface IAddNodeUseCase
{
  Task Execute(AddNodeDTO addNodeDTO);
}