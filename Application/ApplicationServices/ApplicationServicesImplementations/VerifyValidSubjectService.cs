using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;

public class VerifyValidSubjectService (ISubjectRepository subjectRepository): IVerifyValidSubjectService
{
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  public async Task<bool> Execute(string userId, string title)
  {
    Subject subject;
    if (await _subjectRepository.UserHasExceededSubjectsLimit(userId))
      throw new ExceededLimitException("You've reached your maximum ammount of subjects");
    try
    {
      subject = await _subjectRepository.GetSubjectBySubjectNameAndUserId(title, userId);
    }
    catch (EntityNotFoundException)
    {
      return true;
    }

    if (subject != null)
      throw new EntityValidationException($"There's already a subject with name {title}");
    return true;
  }
}