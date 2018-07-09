using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.FunctionalCore;
using MediatR;

namespace Blog.Domain.UseCases
{
    internal static class MediatorExtensions
    {
        internal static async Task PublishAndCommitDomainEvents(this IMediator mediator, Post post, CancellationToken cancellationToken)
        {
            var events = post.GetEvents();
            foreach (var @event in events)
            {
                await mediator.Publish(@event, cancellationToken);
            }
            post.CommitEvents();
        }
    }
}