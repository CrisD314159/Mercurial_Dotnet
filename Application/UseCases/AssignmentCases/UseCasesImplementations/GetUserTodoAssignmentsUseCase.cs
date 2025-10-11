using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;


namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesImplementations;


public class GetUserTodoAssignmentsUseCase(IGetUserAssignmentsByStateService getUserAssignmentsByStateService): IGetUserTodoAssignmentsUseCase
{
  private readonly IGetUserAssignmentsByStateService _getUserAssignmentsByStateService = getUserAssignmentsByStateService;

  public async Task<GetUserAssignmentsDTO> Execute(string userId, int offset, int limit)
  { 
    return await _getUserAssignmentsByStateService.Execute(userId, offset, limit, AssignmentState.TODO);
  }
  
}