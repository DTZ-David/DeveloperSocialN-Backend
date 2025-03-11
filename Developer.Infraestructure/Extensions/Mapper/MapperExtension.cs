using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Developer.Infraestructure.Extensions.Mapper;

public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.Load(ProjectConstant.ApplicationProject));
        return services;
    }
}
