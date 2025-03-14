using Developer.Domain.Ports.Configuration.Localization;
using Developer.Infraestructure.Adapters.Configuration.Localizacion;
using Developer.Infraestructure.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Infraestructure.Extensions.Localization;

public static class LocalizationExtension
{
    public static IServiceCollection AddInternationalization(this IServiceCollection services)
    {
        services.AddLocalization(); 
        services.AddTransient<ILocalizationService, LocalizationService>();
        services.AddScoped<CultureFilter>();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var cultures = new[]
            {
            new CultureInfo("en-US"),
            new CultureInfo("es-CO")
        };
            options.DefaultRequestCulture = new RequestCulture(culture: "es-CO", uiCulture: "es-CO");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;
        });

        return services;
    }

}
