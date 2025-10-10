using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases;

public class UpdateSubjectUseCase(ISubjectRepository subjectRepository,
UserManager<User> userManager,
IValidator<UpdateSubjectDTO> validator)
{
  private readonly  UserManager<User> _userManager = userManager;
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  private readonly IValidator<UpdateSubjectDTO> _validator = validator;
  public async Task Execute(string userId, UpdateSubjectDTO updateSubjectDTO)
  {
    _validator.ValidateAndThrow(updateSubjectDTO);

    var subject = await _subjectRepository.GetSubjectBySubjectIdAndUserId(updateSubjectDTO.SubjectId, userId)
    ?? throw new EntityNotFoundException("Subject not found");

    subject.Name = updateSubjectDTO.Title;
    subject.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _subjectRepository.UpdateSubjectAsync(subject);
  }
}