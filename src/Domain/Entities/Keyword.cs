using Domain.Common;

namespace Domain.Entities;

public class Keyword : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    /// <summary>
    /// Valore della keyword
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Cultura della keyword "lingua-NAZIONE"
    /// </summary>
    public string Culture { get; set; }

    /// <summary>
    /// Quanti Ads sono stati trovati per questa keyword
    /// </summary>
    public int Ranking { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new();
}