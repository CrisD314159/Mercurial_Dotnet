using System.Security;
using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesImplementations;

public class GetCheckListUseCase(
 ICheckListRepository checkListRepository): IGetChecklistUseCase
{
  private readonly ICheckListRepository _checkListRepository = checkListRepository;

  public async Task<GetChecklistDTO> Execute(Guid assignmentId)
  {
    return await _checkListRepository.GetChecklistDTOAsync(assignmentId);
    
  }

}