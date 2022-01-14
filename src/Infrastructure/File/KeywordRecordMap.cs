using System.Globalization;
using Application.Keywords.Queries.ExportKeywords;
using CsvHelper.Configuration;

namespace Infrastructure.File;

public class KeywordRecordMap : ClassMap<KeywordRecord>
{
    public KeywordRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}