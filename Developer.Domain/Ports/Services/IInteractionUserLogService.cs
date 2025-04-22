using Developer.Domain.Entities.Posts;
using Developer.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services
{
    public interface IInteractionUserLogService
    {
        Task<InteractionLog> CreateUserLog(InteractionLog userLog);

    }
}
