using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases;

public class CreateSubject(IValidator<CreateSubjectDTO> validator,
 VerifyValidSubjectService verifyValidSubjectService,
 UserManager<User> userManager,
 ISubjectRepository subjectRepository
 )
{
  private readonly IValidator<CreateSubjectDTO> _validator = validator;
  private readonly VerifyValidSubjectService _verifySubject = verifyValidSubjectService;
  private readonly  UserManager<User> _userManager = userManager;
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  public async Task Execute(string userId, CreateSubjectDTO createSubjectDTO)
  {
    _validator.ValidateAndThrow(createSubjectDTO);
    if (await _verifySubject.Execute(userId, createSubjectDTO.Title))
    {
      var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("User not found");
      if (!user.EmailConfirmed) throw new EntityValidationException("You have not verified your account");
      Subject subject = new()
      {
        Name = createSubjectDTO.Title,
        User = user,
        LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
      };

      await _subjectRepository.CreateSubjectAsync(subject);
    }

  }
}