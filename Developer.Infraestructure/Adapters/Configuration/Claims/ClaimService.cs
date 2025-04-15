using Developer.Domain.Common.Enums;
using Developer.Domain.Ports.Configuration.Claims;
using Developer.Domain.Settings.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Infraestructure.Adapters.Configuration.Claims;

public class ClaimService : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<UserClaim> GetUserClaim()
    {
        var user = new UserClaim(GetClaim(ClaimOption.UserId),
            GetClaim(ClaimOption.Email));
        return Task.FromResult(user);
    }

    public string GetClaim(string name)
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
        {
            var currentUser = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            return currentUser.Claims.First(c => c.Type.Equals(name)).Value;
        }

        return string.Empty;
    }

}
