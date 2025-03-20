


namespace Ordering.Domain.Events;

public record OrderCreatedDomainEvent(Order order)
    : IDomaindEvent;


