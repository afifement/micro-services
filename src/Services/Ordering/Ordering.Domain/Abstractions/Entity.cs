
namespace Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; } = default!;
    public DateTime? CreatedAt { get; set; } = default!;
    public string? CreatedBy { get; set; } = default!;
    public DateTime? LastModified { get; set; } = default!;
    public string? LastModifiedBy { get; set; } = default!;
}
