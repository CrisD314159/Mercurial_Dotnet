using MercurialBackendDotnet.Domain.Model;

namespace MercurialBackendDotnet.Domain.Interfaces;

public interface IUserRepository
{
  Task CreateUserAsync(User user);

  Task<User> GetUserAsync(string userId);

  Task UpdateUserAsync(User user);

  Task DeleteUserAsync(string userId);
  
  
}