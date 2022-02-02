using Domain.Common;

namespace Domain.Entities;

public class Keyword
{
    public int Id { get; set; }

    /// <summary>
    /// Valore della keyword
    /// </summary>
    public string Value { get; set; }

    public string StartingSeed { get; set; }

    /// <summary>
    /// Cultura della keyword "lingua-NAZIONE"
    /// </summary>
    public string Culture { get; set; }

    /// <summary>
    /// Quanti Ads sono stati trovati per questa keyword
    /// </summary>
    public int Ranking { get; set; }

    /// <summary>
    /// Data e ora di quando la keyword è stata scoperta
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Servizio di suggest utilizzato (Google, Bing, DuckDuckGo, Google Trends)
    /// </summary>
    public string SuggestService { get; set; }
}