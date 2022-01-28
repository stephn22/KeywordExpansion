using System.Globalization;

namespace App.Util;

public static class Utilities
{
    public static IEnumerable<KeyValuePair<string, string>> CultureList()
    {
        var cultureList = new Dictionary<string, string>();

        var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        foreach (var c in cultureInfo)
        {
            cultureList.Add(c.Name, c.DisplayName);
        }

        return cultureList.OrderBy(p => p.Value);
    }
}