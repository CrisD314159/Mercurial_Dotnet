using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;

namespace MercurialBackendDotnet.Application.UseCases.CheckListCases.UseCasesImplementations;

public class AddNodeUseCase(IValidator<AddNodeDTO> validator, ICheckListRepository checkListRepository):IAddNodeUseCase
{
  private readonly IValidator<AddNodeDTO> _validator = validator;
  private readonly ICheckListRepository _checkListRepository = checkListRepository;

    public async Task Execute(AddNodeDTO addNodeDTO)
  {
    _validator.ValidateAndThrow(addNodeDTO);

    var list = await _checkListRepository.GetChecklistWithNodesByIdAync(addNodeDTO.ListId) ?? throw new EntityNotFoundException("List not found");

    if (list.ChecklistExceededLimit()) throw new ExceededLimitException("Each list can only have up to 15 elements");

    CheckListItem node = new()
    {
      Content = addNodeDTO.Content,
      IsCompleted = false,
      CheckList = list
    };
    await _checkListRepository.AddNodeToChecklistAsync(node, list);
  }
}