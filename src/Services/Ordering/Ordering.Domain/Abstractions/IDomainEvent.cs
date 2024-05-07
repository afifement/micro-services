namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime Occured => DateTime.Now;

    public string EventType => GetType().AssemblyQualifiedName!;
}
