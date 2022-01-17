using System.Globalization;
using App.ApplicationCore.Keywords.Queries.ExportKeywords;
using CsvHelper.Configuration;

namespace App.Infrastructure.File;

public class KeywordRecordMap : ClassMap<KeywordRecord>
{
    public KeywordRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}