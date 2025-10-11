using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;


namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases.UseCasesImplementations;


public class MarkAssignmentInProgressUseCase(IMarkUserAssignmentByStateService markUserAssignmentByStateService): IMarkAssignmentInProgressUseCase
{
  private readonly IMarkUserAssignmentByStateService _markUserAssignmentByStateService = markUserAssignmentByStateService;

  public async Task Execute(string userId, Guid assignmentId)
  { 
    await _markUserAssignmentByStateService.Execute(userId, assignmentId, AssignmentState.IN_PROGRESS);
  }
  
}