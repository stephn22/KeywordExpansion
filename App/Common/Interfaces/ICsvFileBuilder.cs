using App.Keywords.Queries.ExportKeywords;

namespace App.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildKeywordsFile(IEnumerable<KeywordRecord> records);
}