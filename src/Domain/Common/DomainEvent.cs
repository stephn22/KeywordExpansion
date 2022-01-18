namespace Domain.Common;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}

public abstract class DomainEvent
{
    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.Now;
    }

    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; set; }
}