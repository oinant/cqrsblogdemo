using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.FunctionalCore;
using Blog.Query.DB;
using MediatR;

namespace Blog.Query
{
    public class PostStartedEventHandler : INotificationHandler<PostStarted>
    {
        private readonly InMemoryQueryDB _db;

        public PostStartedEventHandler(InMemoryQueryDB db)
        {
            _db = db;
        }

        public async Task Handle(PostStarted notification, CancellationToken cancellationToken)
        {

            _db.Posts.Add(new PostDTO()
            {
                PostId = notification.AggregateId,
                Title = notification.Title,
                Author = notification.EventInitiatorName,
                CreationDate = notification.EventCreationTime
            });

            _db.Summaries.Add(new PostSummaryDTO()
            {
                PostId = notification.AggregateId,
                Title = notification.Title,
                Author = notification.EventInitiatorName,
                CreationDate = notification.EventCreationTime
            });
        }
    }
}