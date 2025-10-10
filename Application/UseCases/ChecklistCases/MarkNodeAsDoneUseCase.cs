using System.Security;
using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases;

public class MarkNodeAsDoneUseCase(
 ICheckListRepository checkListRepository)
{
  private readonly ICheckListRepository _checkListRepository = checkListRepository;

  public async Task Execute(long nodeId)
  {
    var node = await _checkListRepository.GetCheckListItemAsync(nodeId)
    ?? throw new EntityNotFoundException("Node not found");

    await _checkListRepository.MarkNodeAsDoneAndSaveAsync(node);
  }

}