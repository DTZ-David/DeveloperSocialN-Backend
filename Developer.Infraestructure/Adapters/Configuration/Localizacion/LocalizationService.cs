using Developer.Domain.Ports.Configuration.Localization;
using Developer.Infraestructure.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Infraestructure.Adapters.Configuration.Localizacion;

public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        var assembly = Assembly.Load(ProjectConstant.DomainProject);
        var assemblyName = new AssemblyName(assembly.FullName!);
        _localizer = factory.Create("Common.Resources.SharedResource", assemblyName.Name!);
    }

    public string GetLocalizedByKey(string key)
    {
        return _localizer[key].Value;
    }
}

