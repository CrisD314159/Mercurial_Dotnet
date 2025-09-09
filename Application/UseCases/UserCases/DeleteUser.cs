using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases;


public class DeleteUser(UserManager<User> userManager)
{

  private readonly UserManager<User> _userManager = userManager;
  
  public async Task Execute(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId)
      ?? throw new EntityNotFoundException("User not found");

    var result = await _userManager.DeleteAsync(user);
    if (!result.Succeeded)
    {
      throw new InternalServerException("An error occurred while trying to delete user");
    }
  }
}