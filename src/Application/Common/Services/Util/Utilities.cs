using System.Globalization;

namespace Application.Common.Services.Util;

public static class Utilities
{
    public static IEnumerable<KeyValuePair<string, string>> CultureList()
    {
        var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        var cultureList = cultureInfo.ToDictionary(c => c.Name, c => c.DisplayName);

        return cultureList.OrderBy(p => p.Value);
    }
}