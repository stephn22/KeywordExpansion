using Application.Common.Interfaces;
using Application.Keywords.Queries.ExportKeywords;
using CsvHelper;
using System.Globalization;

namespace Infrastructure.File;

public class CsvFileReader : ICsvFileReader
{
    public IEnumerable<KeywordRecord> ReadKeywordsFromFile(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<KeywordRecord>().ToList();

        return records;
    }
}