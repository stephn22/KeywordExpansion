using App.Domain.Common;
using App.Domain.Entities;

namespace App.Domain.Events;

public class KeywordCreatedEvent : DomainEvent
{
    public KeywordCreatedEvent(Keyword keyword)
    {
        Keyword = keyword;
    }

    public Keyword Keyword { get; set; }
}