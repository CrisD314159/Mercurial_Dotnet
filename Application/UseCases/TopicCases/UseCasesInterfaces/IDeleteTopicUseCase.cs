using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.TopicCases.UseCasesInterfaces;

public interface IDeleteTopicUseCase
{
  Task Execute(string userId, long topicId);
}