using Developer.Domain.Settings.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Configuration.Claims;

public interface IClaimService
{
    Task<UserClaim> GetUserClaim();
}
