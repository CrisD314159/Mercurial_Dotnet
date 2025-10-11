using MercurialBackendDotnet.Domain.Model;

namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface ICreateThirdPartyUserUseCase
{
  Task<User> Execute(string email, string name);
}