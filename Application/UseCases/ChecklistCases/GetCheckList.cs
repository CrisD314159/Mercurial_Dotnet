using System.Security;
using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases;

public class GetCheckList(
 ICheckListRepository checkListRepository)
{
  private readonly ICheckListRepository _checkListRepository = checkListRepository;

  public async Task<GetChecklistDTO> Execute(Guid assignmentId)
  {
    return await _checkListRepository.GetChecklistDTOAsync(assignmentId);
    
  }

}