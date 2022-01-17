using App.Domain.Common;

namespace App.ApplicationCore.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}