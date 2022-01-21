using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Keywords.Queries.ExportKeywords;

public class KeywordRecord : IMapFrom<Keyword>
{
    public int id { get; set; }
    public string keyword { get; set; }
    public string country { get; set; }
    public string  lang { get; set; }
}