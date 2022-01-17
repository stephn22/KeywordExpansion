using System.Globalization;
using App.Common.Interfaces;
using App.Keywords.Queries.ExportKeywords;
using CsvHelper;

namespace App.File;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildKeywordsFile(IEnumerable<KeywordRecord> records)
    {
        using var stream = new MemoryStream();
        using var streamWriter = new StreamWriter(stream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.Context.RegisterClassMap<KeywordRecordMap>();
        csvWriter.WriteRecords(records);

        return stream.ToArray();
    }
}