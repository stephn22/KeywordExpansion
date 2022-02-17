using Application.Keywords.Queries.ExportKeywords;
using CsvHelper.Configuration;
using System.Globalization;

namespace Infrastructure.File;

public class KeywordRecordMap : ClassMap<KeywordRecord>
{
    public KeywordRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}