using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Drivers;
using Blog.Domain.FunctionalCore;
using MediatR;

namespace Blog.Domain.UseCases
{
    public class StartPost : INotificationHandler<StartPostCommand>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediator _mediator;

        public StartPost(IPostRepository postRepository, IMediator mediator)
        {
            _postRepository = postRepository;
            _mediator = mediator;
        }

        public async Task Handle(StartPostCommand command, CancellationToken cancellationToken)
        {
            var post = Post.Start(command.Author, command.Title);
            _postRepository.Add(post);
            _postRepository.Save();

            await _mediator.PublishAndCommitDomainEvents(post, cancellationToken);
        }
    }

    public class StartPostCommand : INotification
    {
        public StartPostCommand(string author, string title)
        {
            Author = author;
            Title = title;
        }

        public string Title { get; }

        public string Author { get; }
    }
}
