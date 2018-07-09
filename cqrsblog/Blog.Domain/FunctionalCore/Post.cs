using System;
using System.Collections.Generic;

namespace Blog.Domain.FunctionalCore
{
    public class Post
    {
        private string _title;
        private string _content;
        private ICollection<string> _tags = new HashSet<string>();
        private string _author;
        private PostStatus _status;

        private List<IDomainEvent> _events = new List<IDomainEvent>();
        private readonly Guid _postId;

        private Post()
        {
            _postId = Guid.NewGuid();
        }

        public static Post Start(string author, string title)
        {
            var post = new Post
            {
                _author = author,
                _title = title,
                _status = PostStatus.Draft
            };

            post._events.Add(new PostStarted(post._postId, author, title));

            return post;
        }

        public void ChangeTitle(string changedBy, string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new PostTitleChangedWithBlankTitleException(this, changedBy);

            if (newTitle != _title)
            {
                _title = newTitle;
                _events.Add(new PostTitleChanged(_postId, changedBy, _title));
            }
        }

        public Guid GetPostId() => _postId;

        public IReadOnlyList<IDomainEvent> GetEvents()
        {
            return _events.AsReadOnly();
        }

        public void CommitEvents()
        {
            _events.Clear();
        }
    }
}
