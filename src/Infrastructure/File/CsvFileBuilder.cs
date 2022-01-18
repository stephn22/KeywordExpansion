using System.Globalization;
using Application.Common.Interfaces;
using Application.Keywords.Queries.ExportKeywords;
using CsvHelper;

namespace Infrastructure.File;

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