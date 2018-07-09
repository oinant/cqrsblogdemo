using System;

namespace Blog.Domain.FunctionalCore
{
    public class PostStarted : IDomainEvent
    {
        public PostStarted(Guid postId, string author, string title)
        {
            EventCreationTime = DateTime.UtcNow;
            EventInitiatorName = author;
            AggregateId = postId;
            Title = title;
        }


        public DateTime EventCreationTime { get; }

        public string EventInitiatorName { get; }

        public Guid AggregateId { get; }

        public string Title { get; }
    }
}