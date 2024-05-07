namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainsEvent = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainsEvent.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainsEvent.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequeuedEvents = _domainsEvent.ToArray();
        _domainsEvent.Clear();
        return dequeuedEvents;
    }
     
}
