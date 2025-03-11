using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Services;

public interface IAccountService
{
    public Task<string> ValidateMobileApp(string email, string password);
}
