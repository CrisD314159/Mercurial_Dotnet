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

public class RemoveNodeUseCase(
 ICheckListRepository checkListRepository):IRemoveNodeUseCase
{
  private readonly ICheckListRepository _checkListRepository = checkListRepository;

  public async Task Execute(long nodeId)
  {

    var node = await _checkListRepository.GetCheckListItemAsync(nodeId)
    ?? throw new EntityNotFoundException("Node not found");

    var checkList = await _checkListRepository.GetChecklistWithNodesByIdAync(node.CheckListId)
    ?? throw new EntityNotFoundException("Checklist Not found");

    await _checkListRepository.RemoveNodeFromChecklistAsync(node, checkList);
  }

}