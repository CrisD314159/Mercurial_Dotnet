using FluentValidation;
using MercurialBackendDotnet.Application.ApplicationExceptions;
using MercurialBackendDotnet.Application.ExternalServices;
using MercurialBackendDotnet.Domain.DomainExceptions;
using MercurialBackendDotnet.Domain.Enums;
using MercurialBackendDotnet.Domain.Model;
using MercurialBackendDotnet.Presentation.Dto.InputDTO;
using Microsoft.AspNetCore.Identity;

namespace MercurialBackendDotnet.Application.UseCases.UserCases;


public class CreateRegularUser(UserManager<User> userManager,
IValidator<CreateUserDTO> validator,
IEmailService emailService)
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly IValidator<CreateUserDTO> _validator = validator;
  private readonly IEmailService _emailService = emailService;

  public async Task Execute(CreateUserDTO createUserDTO)
  {
    if (await _userManager.FindByEmailAsync(createUserDTO.Email) != null)
      throw new EntityAlreadyExistsException("User already exists");

    _validator.ValidateAndThrow(createUserDTO);

    var cleanName = createUserDTO.Name.Trim().Replace(" ", "");

    User user = new()
    {
      Id = Guid.NewGuid().ToString(),
      Name = createUserDTO.Name,
      State = UserState.NOT_VERIFIED,
      ProfilePicture = $"https://api.dicebear.com/9.x/thumbs/svg?seed={createUserDTO.Name}",
      LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
      VerificationCode = new Random().Next(1000, 9999).ToString(),
      UserName = cleanName,
      Email = createUserDTO.Email,
      EmailConfirmed = false,
      IsThirdPartyUser = false
    };


    var result = await _userManager.CreateAsync(user, createUserDTO.Password);
    if (!result.Succeeded)
    {
      throw new InternalServerException(string.Join(";", result.Errors.Select(e => e.Description)));
    }
    await _emailService.SendAccountCreatedVerificationCode(createUserDTO.Name, createUserDTO.Email, user.VerificationCode);

  }
  
}