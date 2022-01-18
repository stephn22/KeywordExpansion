using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class KeywordCreatedEvent : DomainEvent
{
    public KeywordCreatedEvent(Keyword keyword)
    {
        Keyword = keyword;
    }

    public Keyword Keyword { get; set; }
}