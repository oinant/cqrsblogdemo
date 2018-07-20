using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Drivers;
using Blog.Domain.FunctionalCore;
using MediatR;

namespace Blog.Domain.UseCases
{
    public class ChangeTitle : INotificationHandler<ChangeTitleCommand>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediator _mediator;

        public ChangeTitle(IPostRepository postRepository, IMediator mediator)
        {
            _postRepository = postRepository;
            _mediator = mediator;
        }

        public async Task Handle(ChangeTitleCommand command, CancellationToken cancellationToken)
        {
            var post = _postRepository.GetById(command.PostId);
            
            post.ChangeTitle(command.ChangedBy, command.Title);

            _postRepository.Save(post);

            await _mediator.PublishAndCommitDomainEvents(post, cancellationToken);
        }
    }

    public class ChangeTitleCommand : INotification
    {
        public ChangeTitleCommand(Guid postId, string changedBy, string title)
        {
            PostId = postId;
            ChangedBy = changedBy;
            Title = title;
        }

        public Guid PostId { get; }

        public string Title { get; }

        public string ChangedBy { get; }
    }
}