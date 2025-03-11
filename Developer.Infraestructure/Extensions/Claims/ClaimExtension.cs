using Developer.Domain.Ports.Configuration.Claims;
using Developer.Infraestructure.Adapters.Configuration.Claims;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Infraestructure.Extensions.Claims;

public static class ClaimExtension
{
    public static IServiceCollection AddClaims(this IServiceCollection services)
    {
        services.AddTransient<IClaimService, ClaimService>();
        return services;
    }
}
