using Developer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Developer.Domain.Ports.Services;

 public interface IUserRegisterServices
    {
        Task<RegisterUser> CreateUserAsync (RegisterUser usuario);

        Task<IEnumerable<RegisterUser>> GetAllUsersAsync();
    }
