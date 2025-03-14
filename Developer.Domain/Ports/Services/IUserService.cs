using Developer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User usuario);
}
