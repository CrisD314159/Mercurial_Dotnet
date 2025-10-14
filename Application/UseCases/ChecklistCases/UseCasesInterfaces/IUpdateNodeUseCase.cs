using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;

public interface IUpdateNodeUseCase
{
  Task Execute(UpdateNodeDTO updateNodeDTO);
}
