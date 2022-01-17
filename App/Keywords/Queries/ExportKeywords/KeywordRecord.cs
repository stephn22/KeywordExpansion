using App.Common.Mappings;
using App.Entities;

namespace App.Keywords.Queries.ExportKeywords;

public class KeywordRecord : IMapFrom<Keyword>
{
    public string Value { get; set; }
    public string Culture { get; set; }
    public int Ranking { get; set; }
}