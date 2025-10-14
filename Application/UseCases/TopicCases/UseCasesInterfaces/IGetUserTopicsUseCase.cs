using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesInterfaces;

public interface IGetUserTopicsUseCase
{
  Task<GetUserTopicsDTO> Execute(string userId, int offset, int limit);
}