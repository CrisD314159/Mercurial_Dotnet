
namespace MercurialBackendDotnet.Application.UseCases.UserCases.UseCasesInterfaces;

public interface IDeleteUserUseCase
{
  Task Execute(string userId);
}