using System;

namespace Blog.Domain.FunctionalCore
{
    public class PostTitleChanged : IDomainEvent
    {
        public PostTitleChanged(Guid postId, string changedBy, string newTitle)
        {
            EventCreationTime = DateTime.UtcNow;
            EventInitiatorName = changedBy;
            AggregateId = postId;
            NewTitle = newTitle;
        }

        public DateTime EventCreationTime { get; }

        public string EventInitiatorName { get; }

        public Guid AggregateId { get; }

        public string NewTitle { get; }
    }
}