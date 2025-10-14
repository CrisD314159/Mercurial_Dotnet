using MercurialBackendDotnet.Presentation.Dto.OutputDTO;

namespace MercurialBackendDotnet.Application.ApplicationServices.ApplicationServicesInterfaces;

public interface IGenerateThirdPartyTokenService
{
  Task<LoginResponseDTO> Execute(string id, string email);
}