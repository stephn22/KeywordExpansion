using Application.Keywords.Queries.ExportKeywords;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ICsvFileReader
{
    IEnumerable<KeywordRecord> ReadKeywordsFromFile(string path);
}