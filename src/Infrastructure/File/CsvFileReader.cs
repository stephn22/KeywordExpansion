using System.Globalization;
using Application.Common.Interfaces;
using Application.Keywords.Queries.ExportKeywords;
using CsvHelper;

namespace Infrastructure.File;

public class CsvFileReader : ICsvFileReader
{
    public IEnumerable<KeywordRecord> ReadKeywordsFromFile(string path)
    {
        using var reader = new StreamReader(System.IO.File.OpenRead(Path.GetFullPath(path))); // FIXME: FileNotFoundException
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<KeywordRecord>();

        return records;
    }
}