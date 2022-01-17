using App.ApplicationCore.Keywords.Queries.ExportKeywords;

namespace App.ApplicationCore.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildKeywordsFile(IEnumerable<KeywordRecord> records);
}