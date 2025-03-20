
namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomaindEvent> _domainEvents = new ();
        public IReadOnlyList<IDomaindEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomaindEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IDomaindEvent[] ClearDomainEvents()
        {
            IDomaindEvent[] domainEvents = _domainEvents.ToArray();

            _domainEvents.Clear();

            return domainEvents;
        }

    }
}
