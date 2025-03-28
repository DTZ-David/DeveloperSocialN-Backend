using Developer.Infraestructure.Extensions;
using Developer.Infraestructure.Extensions.Claims;
using Developer.Infraestructure.Extensions.Feature;
using Developer.Infraestructure.Extensions.JsonWebToken;
using Developer.Infraestructure.Extensions.Localization;
using Developer.Infraestructure.Extensions.Mapper;
using Developer.Infraestructure.Extensions.Mediador;
using Developer.Infraestructure.Extensions.Middleware;
using Developer.Infraestructure.Extensions.Persistence;
using Developer.Infraestructure.Extensions.Services;
using Developer.Infraestructure.Extensions.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Developer.Infraestructure;


public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInternationalization() // Aquí debería registrarse ILocalizationService
            .AddMediator()
            .AddFeature()
            .AddJsonWebToken(configuration)
            .AddApplicationServices(ProjectConstant.DomainProject)
            .AddSwagger()
            .AddMapper()
            .AddPersistence(configuration)
            .AddClaims()    
            .AddAuthorization()
            .AddCustomMiddleware();

    }

    public static void UseInfrastructure
    (
        this IApplicationBuilder builder,
        IWebHostEnvironment environment,
        IConfigurationBuilder configuration
    )
    {
        builder
            .UseSwagger(environment)
            .UseAuthentication()
            .UseAuthorization()
            .UseCustomMiddleware();
    }
}
