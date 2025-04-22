using Developer.Domain.Entities;
using Developer.Domain.Entities.User;

namespace Developer.Domain.Ports.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User usuario);
    Task<User> GetUserById(string id);
    Task<User> UpdateUser(User user);

}
