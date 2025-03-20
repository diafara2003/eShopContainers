﻿
using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomaindEvent:INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}
