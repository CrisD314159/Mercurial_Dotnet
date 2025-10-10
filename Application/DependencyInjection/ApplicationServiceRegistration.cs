using MercurialBackendDotnet.Application.UseCases.Account;
using MercurialBackendDotnet.Application.UseCases.AssignmentCases;
using MercurialBackendDotnet.Application.UseCases.CheckListCases;
using MercurialBackendDotnet.Application.UseCases.SubjectCases;
using MercurialBackendDotnet.Application.UseCases.TopicCases;
using MercurialBackendDotnet.Application.UseCases.UserCases;


namespace MercurialBackendDotnet.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
  // This class will allow dependency injection on Program.cs file using Scrutor package
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    // Account Use cases
    services.AddScoped<LoginUseCase>();
    services.AddScoped<LoginUsingGoogleUseCase>();
    services.AddScoped<LogoutUseCase>();
    services.AddScoped<RefreshToken>();

    // Assignment use cases
    services.AddScoped<CreateAssignmentUseCase>();
    services.AddScoped<DeleteAssignmentUseCase>();
    services.AddScoped<GetUserDoneAssignmentsUseCase>();
    services.AddScoped<GetUserTodoAssignmentsUseCase>();
    services.AddScoped<MarkAssignmentAsDoneUseCase>();
    services.AddScoped<MarkAssignmentInProgressUseCase>();
    services.AddScoped<MarkAssignmentTodoUseCase>();
    services.AddScoped<UpdateAssignmentUseCase>();

    // Checklist use cases
    services.AddScoped<AddNodeUseCase>();
    services.AddScoped<CreateChecklistUseCase>();
    services.AddScoped<DeleteChecklistUseCase>();
    services.AddScoped<GetCheckListUseCase>();
    services.AddScoped<MarkNodeAsDoneUseCase>();
    services.AddScoped<RemoveNodeUseCase>();
    services.AddScoped<UnmarkNodeAsDoneUseCase>();
    services.AddScoped<UpdateNodeUseCase>();

    // Subject use cases
    services.AddScoped<CreateSubjectUseCase>();
    services.AddScoped<DeleteSubjectUseCase>();
    services.AddScoped<GetUserSubjectsUseCase>();
    services.AddScoped<UpdateSubjectUseCase>();

    // Topic use cases
    services.AddScoped<CreateTopicUseCase>();
    services.AddScoped<DeleteTopicUseCase>();
    services.AddScoped<GetUserTopicsUseCase>();
    services.AddScoped<UpdateTopicUseCase>();

    // User use cases
    services.AddScoped<ChangeUserPasswordUseCase>();
    services.AddScoped<CreateRegularUserUseCase>();
    services.AddScoped<CreateThirdPartyUserUseCase>();
    services.AddScoped<DeleteUserUseCase>();
    services.AddScoped<GetUserOverviewUseCase>();
    services.AddScoped<RecoverUserAccountUseCase>();
    services.AddScoped<UpdateUserUseCase>();
    services.AddScoped<VerifyUserUseCase>();


    return services;
  }
}
