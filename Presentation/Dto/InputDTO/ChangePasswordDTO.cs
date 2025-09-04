namespace MercurialBackendDotnet.Presentation.Dto.InputDTO;

public record ChangePasswordDTO(string Email, string Password, string Code);