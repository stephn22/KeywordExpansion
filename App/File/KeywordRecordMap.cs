using System.Globalization;
using App.Keywords.Queries.ExportKeywords;
using CsvHelper.Configuration;

namespace App.File;

public class KeywordRecordMap : ClassMap<KeywordRecord>
{
    public KeywordRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}