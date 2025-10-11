using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Infrastructure.RepositoriesImpl;

namespace MercurialBackendDotnet.Infrastructure.InfrastructureInjection;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    services.AddScoped<IAssignmentRepository, AssignmentRepositoryImpl>();
    services.AddScoped<ISubjectRepository, SubjectRepositoryImpl>();
    services.AddScoped<ICheckListRepository, CheckListRepositoryImpl>();
    services.AddScoped<ISessionsRepository, SessionsRepositoryImpl>();
    services.AddScoped<ISubjectRepository, SubjectRepositoryImpl>();
    services.AddScoped<ITopicRepository, TopicRepositoryImpl>();
    return services;
  }
}
