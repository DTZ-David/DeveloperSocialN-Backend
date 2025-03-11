using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Configuration.JsonWebToken;

public interface IJwtService
{
   string BuildToken(List<string> claimsValue);
}
