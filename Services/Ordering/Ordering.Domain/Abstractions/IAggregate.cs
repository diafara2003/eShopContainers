

namespace Ordering.Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T>
{

}

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomaindEvent> DomainEvents { get; }
    IDomaindEvent[] ClearDomainEvents();
}
