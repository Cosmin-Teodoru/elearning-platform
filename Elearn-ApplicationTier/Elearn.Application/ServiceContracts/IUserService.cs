using Elearn.Shared.Models;

namespace Elearn.Application.ServiceContracts;

public interface IUserService
{
    Task<User?> GetUserByNameAsync(string name);
    Task<User> CreateNewUserAsync(User user);
     Task<User?> GetUserByIdAsync(long id);
    Task<User> UpdateUserAsync(User updated);
    Task<User?> GetUserByUsernameAsync(string username);
    Task DeleteUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();

}
