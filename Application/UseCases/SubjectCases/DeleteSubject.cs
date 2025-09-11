using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases;

public class DeleteSubject(ISubjectRepository subjectRepository)
{

  private readonly ISubjectRepository _subjectRepository = subjectRepository;

  public async Task Execute(string userid, long subjectId)
  {
    var subject = await _subjectRepository.GetSubjectBySubjectIdAndUserId(subjectId, userid)
    ?? throw new EntityNotFoundException("Subject not found");

    await _subjectRepository.DeleteSubjectAsync(subject);
  }

}