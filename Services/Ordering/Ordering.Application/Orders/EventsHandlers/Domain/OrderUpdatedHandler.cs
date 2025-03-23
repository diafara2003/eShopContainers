namespace Ordering.Application.Orders.EventsHandlers.Domain;

public class OrderUpdatedHandler(ILogger<OrderUpdatedHandler> loger)
    : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        loger.LogInformation("domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
