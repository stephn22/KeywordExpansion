using Application.Keywords.Queries.ExportKeywords;

namespace Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildKeywordsFile(IEnumerable<KeywordRecord> records);
}