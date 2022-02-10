using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Keywords.Queries.ExportKeywords;

public class KeywordRecord : IMapFrom<Keyword>
{
    public int Id { get; set; }
    public string Value { get; set; }
    public string Culture { get; set; }
    public string  Ranking { get; set; }
}