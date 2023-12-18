namespace Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public TId Id { get; protected set; }
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

#pragma warning disable CS8618
    protected Entity() { }
#pragma warning restore CS8618

    protected Entity(TId id) 
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null 
            && obj is Entity<TId> entity
            && entity.Id.Equals(Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?) other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
