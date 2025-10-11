using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesInterfaces;

public interface IUpdateTopicUseCase
{
  Task Execute(string userId, UpdateTopicDTO updateTopicDTO);
}