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
   
    public IClaimService ClaimsService { get; }

    public UnitOfWork(IAccountService accountService,
                     
                      IClaimService claimsService)
    {
        AccountService = accountService;
       
        ClaimsService = claimsService;
    }
}
