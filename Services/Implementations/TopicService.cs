using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Services.Interfaces;

namespace MercurialBackendDotnet.Services.Implementations;

public class TopicService(MercurialDBContext dBContext) : ITopicService
{

  private readonly MercurialDBContext _dbContext = dBContext;
  public void CreateTopic(CreateTopicDTO createTopicDTO)
  {
    throw new NotImplementedException();
  }

  public void DeleteTopic(string topicId)
  {
    throw new NotImplementedException();
  }

  public GetUserTopicsDTO GetUserTopics(string userId, int offset, int limit)
  {
    throw new NotImplementedException();
  }

  public void UpdateTopic(UpdateTopicDTO updateTopicDTO)
  {
    throw new NotImplementedException();
  }
}