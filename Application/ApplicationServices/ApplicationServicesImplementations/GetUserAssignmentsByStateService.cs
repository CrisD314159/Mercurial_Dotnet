using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Entities;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Interfaces;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesImplementations;

public class GetUserAssignmentsByStateService(
IAssignmentRepository assignmentRepository,
UserManager<User> userManager
): IGetUserAssignmentsByStateService
{
  private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
  private readonly UserManager<User> _userManager = userManager ;
  public async Task<GetUserAssignmentsDTO> Execute(string userId, int offset, int limit, AssignmentState state)
  {
    var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");

    if (!user.EmailConfirmed) throw new EntityValidationException("You have not verified your account");

    if (!await _userManager.IsEmailConfirmedAsync(user)) throw new UnauthorizedException("You're not verified");
    var assignments = await _assignmentRepository.GetUserAssignmentsAsync(userId, offset, limit, state);

    return new GetUserAssignmentsDTO(assignments);
  }
}