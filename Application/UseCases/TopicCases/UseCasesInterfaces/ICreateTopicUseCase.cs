using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesInterfaces;

public interface ICreateTopicUseCase
{
  Task Execute(string userId, CreateTopicDTO createTopicDTO);
}