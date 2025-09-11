using System.Security;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.ApplicationServices;

public class VerifyValidSubjectService (ISubjectRepository subjectRepository)
{
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
  public async Task<bool> Execute(string userId, string title)
  {
    if (await _subjectRepository.UserHasExceededSubjectsLimit(userId))
      throw new ExceededLimitException("You've reached your maximum ammount of subjects");
    var subject = await _subjectRepository.GetSubjectBySubjectNameAndUserId(title, userId);
    if(subject != null)
      throw new VerificationException($"There's already a subject with name {title}");
    return true;
  }
}