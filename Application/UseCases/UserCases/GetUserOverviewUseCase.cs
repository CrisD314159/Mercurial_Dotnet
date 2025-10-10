using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.OutputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases;


public class GetUserOverviewUseCase(UserManager<User> userManager)
{
  private readonly UserManager<User> _userManager = userManager;

  public async Task<GetUserDTO> Execute(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId)
      ?? throw new EntityNotFoundException("User does not exists");

    return new GetUserDTO(
      user.Id,
      user.Email ?? throw new EntityNotFoundException("Email not found"),
      user.ProfilePicture,
      user.Name
    );
  }
  
}