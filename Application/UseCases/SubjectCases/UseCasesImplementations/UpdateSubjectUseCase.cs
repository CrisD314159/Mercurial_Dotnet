using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Application.UseCases.SubjectCases.UseCasesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases.UseCasesImplementations;

public class UpdateSubjectUseCase(ISubjectRepository subjectRepository,
UserManager<User> userManager,
IValidator<UpdateSubjectDTO> validator,
IVerifyValidSubjectService verifyValidSubjectService
) :IUpdateSubjectUseCase
{
  private readonly  UserManager<User> _userManager = userManager;
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  private readonly IValidator<UpdateSubjectDTO> _validator = validator;
  private readonly IVerifyValidSubjectService _verifySubject = verifyValidSubjectService;
  public async Task Execute(string userId, UpdateSubjectDTO updateSubjectDTO)
  {
    _validator.ValidateAndThrow(updateSubjectDTO);

    await _verifySubject.Execute(userId, updateSubjectDTO.Title);

    var subject = await _subjectRepository.GetSubjectBySubjectIdAndUserId(updateSubjectDTO.SubjectId, userId)
    ?? throw new EntityNotFoundException("Subject not found");

    subject.Name = updateSubjectDTO.Title;
    subject.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _subjectRepository.UpdateSubjectAsync(subject);
  }
}