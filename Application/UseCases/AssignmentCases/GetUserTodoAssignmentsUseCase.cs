using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases;


public class GetUserTodoAssignmentsUseCase(GetUserAssignmentsByStateService getUserAssignmentsByStateService)
{
  private readonly GetUserAssignmentsByStateService _getUserAssignmentsByStateService = getUserAssignmentsByStateService;

  public async Task<GetUserAssignmentsDTO> Execute(string userId, int offset, int limit)
  { 
    return await _getUserAssignmentsByStateService.Execute(userId, offset, limit, AssignmentState.TODO);
  }
  
}