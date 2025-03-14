using Developer.Domain.Ports.Configuration.Claims;
using Developer.Domain.Ports.Services;
using Developer.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Infraestructure.Adapters;

public class UnitOfWork : IUnitOfWork
{
    public IAccountService AccountService { get; }
    public IUserService UserService { get; }
    public IClaimService ClaimsService { get; }

    public UnitOfWork(IAccountService accountService,
                     IUserService userService,
                      IClaimService claimsService)
    {
        AccountService = accountService;
        UserService = userService;
        ClaimsService = claimsService;
    }
}
