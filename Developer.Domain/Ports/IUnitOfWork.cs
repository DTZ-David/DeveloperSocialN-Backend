using Developer.Domain.Ports.Configuration.Claims;
using Developer.Domain.Ports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports;

public interface IUnitOfWork
{
    IAccountService AccountService { get; }
    IClaimService ClaimsService { get; }
    IUserService UserService { get; }

    IPostService PostService { get; }
}
