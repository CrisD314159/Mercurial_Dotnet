using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.AssignmentCases;


public class MarkAssignmentTodo(MarkUserAssignmentByStateService markUserAssignmentByStateService)
{
  private readonly MarkUserAssignmentByStateService _markUserAssignmentByStateService = markUserAssignmentByStateService;

  public async Task Execute(string userId, Guid assignmentId)
  { 
    await _markUserAssignmentByStateService.Execute(userId, assignmentId, AssignmentState.TODO);
  }
  
}