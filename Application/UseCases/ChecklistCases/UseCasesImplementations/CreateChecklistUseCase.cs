using System.Security;
using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesImplementations;

public class CreateChecklistUseCase(
 ICheckListRepository checkListRepository,
 IAssignmentRepository assignmentRepository): ICreateChecklistUseCase
{
  private readonly ICheckListRepository _checkListRepository = checkListRepository;
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;

  public async Task Execute(Guid assignmentId)
  {
    var assignment = await _assignmentRepository.GetAssignmentByIdAync(assignmentId)
    ?? throw new EntityNotFoundException("Assignment not found");

    if(assignment.HasChecklist) throw new EntityValidationException("This assignment already has a checklist");

    CheckList checkList = new (){
      Assignment = assignment,
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
    };
    await _checkListRepository.CreateCheckListAsync(checkList, assignment);
      
  }

}