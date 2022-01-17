using App.ApplicationCore.Common.Mappings;
using App.Domain.Entities;

namespace App.ApplicationCore.Keywords.Queries.ExportKeywords;

public class KeywordRecord : IMapFrom<Keyword>
{
    public string Value { get; set; }
    public string Culture { get; set; }
    public int Ranking { get; set; }
}