using Developer.Domain.Entities;
using Developer.Domain.Entities.User;

namespace Developer.Domain.Ports.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User usuario);
}
