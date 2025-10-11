
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Infrastructure.ExternalServicesImpl;

namespace MercurialBackendDotnet.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
  // This class will allow dependency injection on Program.cs file using Scrutor package
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    // Other services
    services.AddScoped<IEmailService, EmailServiceImpl>();
    services.AddScoped<IJwtService, JWTServiceImpl>();
    services.AddScoped<IPushNotificationService, PushNotificationServiceImpl>();
    services.AddScoped<IGenerateSessionService, GenerateSessionService>();
    services.AddScoped<IGenerateThirdPartyTokenService, GenerateThirdPartyTokenService>();
    services.AddScoped<IGetUserAssignmentsByStateService, GetUserAssignmentsByStateService>();
    services.AddScoped<IMarkUserAssignmentByStateService, MarkUserAssignmentByStateService>();
    services.AddScoped<IValidateAndGenerateValidUserService, ValidateAndGenerateValidUsernameService>();
    services.AddScoped<IVerifyValidAssignmentService, VerifyValidAssignmentService>();
    services.AddScoped<IVerifyValidSubjectService, VerifyValidSubjectService>();
    services.AddScoped<IVerifyValidTopicService, VerifyValidTopicService>();

    return services;
  }
}
