using System.Security;
using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases;

public class DeleteChecklist(
 ICheckListRepository checkListRepository,
 IAssignmentRepository assignmentRepository)
{
  private readonly ICheckListRepository _checkListRepository = checkListRepository;
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;

  public async Task Execute(long listId)
  {
    var checkList = await _checkListRepository.GetChecklistWithNodesByIdAync(listId)
    ?? throw new EntityNotFoundException("Checklist not found");
    

    var assignment = await _assignmentRepository.GetAssignmentByIdAync(checkList.AssignmentId)
    ?? throw new EntityNotFoundException("Assignment not found");

    await _checkListRepository.DeleteChecklistAsync(checkList, assignment);
  }

}