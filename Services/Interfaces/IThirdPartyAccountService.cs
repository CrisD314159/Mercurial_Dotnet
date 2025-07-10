using MercurialBackendDotnet.Model;

namespace MercurialBackendDotnet.Services.Interfaces;

public interface IThirdPartyAccountService
{
  Task<User> CreateThirdPartyUser(string email, string name);
}