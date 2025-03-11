using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Common.Helpers;


public static class GetCulture
{
    public static string GetLanguage()
    {
        var cultureInfo = CultureInfo.CurrentCulture;

        var languageCode = cultureInfo.TwoLetterISOLanguageName;

        return languageCode;
    }
}
