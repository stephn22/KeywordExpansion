using Application.Keywords.Queries.ExportKeywords;

namespace Application.Common.Interfaces;

public interface ICsvFileReader
{
    IEnumerable<KeywordRecord> ReadKeywordsFromFile(string path);
}