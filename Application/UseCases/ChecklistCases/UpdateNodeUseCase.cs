using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases;

public class UpdateNodeUseCase(IValidator<UpdateNodeDTO> validator, ICheckListRepository checkListRepository)
{
  private readonly IValidator<UpdateNodeDTO> _validator = validator;
  private readonly ICheckListRepository _checkListRepository = checkListRepository;

  public async Task Execute(UpdateNodeDTO updateNodeDTO)
  {
    _validator.ValidateAndThrow(updateNodeDTO);

    var node = await _checkListRepository.GetCheckListItemAsync(updateNodeDTO.NodeId)
    ?? throw new EntityNotFoundException("Node not found");

    node.Content = updateNodeDTO.Content;
    await _checkListRepository.UpdateNodeAsync(node);
  }
}