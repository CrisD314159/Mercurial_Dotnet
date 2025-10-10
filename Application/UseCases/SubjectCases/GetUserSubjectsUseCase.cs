using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.SubjectCases;

public class GetUserSubjectsUseCase(ISubjectRepository subjectRepository, UserManager<User> userManager)
{
  private readonly  UserManager<User> _userManager = userManager;
  private readonly ISubjectRepository _subjectRepository = subjectRepository;
    public async Task<GetUserSubjectsDTO> Execute(string userId, int offset, int limit)
  {
    var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");

    if (!await _userManager.IsEmailConfirmedAsync(user)) throw new EntityValidationException("You're not verified");

    var subjectsList = await _subjectRepository.GetUserSubjectsAsync(userId, offset, limit);

    return new GetUserSubjectsDTO(subjectsList);

  }
}