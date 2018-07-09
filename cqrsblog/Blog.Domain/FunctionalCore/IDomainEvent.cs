using System;
using MediatR;

namespace Blog.Domain.FunctionalCore
{
    public interface IDomainEvent : INotification
    {
        DateTime EventCreationTime { get; }
        string EventInitiatorName { get; }
        Guid AggregateId { get; }
    }
}