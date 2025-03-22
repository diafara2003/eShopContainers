
namespace Builingblock.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
    public string EventTYpe => GetType().AssemblyQualifiedName;
}
