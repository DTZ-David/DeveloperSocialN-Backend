using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Ports.Configuration.Localization;

public interface ILocalizationService
{
    string GetLocalizedByKey(string key);
}
