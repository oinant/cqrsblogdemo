using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.FunctionalCore;
using Blog.Query.DB;
using MediatR;

namespace Blog.Query
{
    public class PostTitleChangedEventHandler : INotificationHandler<PostTitleChanged>
    {
        private readonly InMemoryQueryDB _db;

        public PostTitleChangedEventHandler(InMemoryQueryDB db)
        {
            _db = db;
        }

        public async Task Handle(PostTitleChanged notification, CancellationToken cancellationToken)
        {
            var post = _db.Posts.Single(p => p.PostId == notification.AggregateId);
            post.Title = notification.NewTitle;

            var summary = _db.Summaries.Single(p => p.PostId == notification.AggregateId);
            summary.Title = notification.NewTitle;
        }
    }
}
