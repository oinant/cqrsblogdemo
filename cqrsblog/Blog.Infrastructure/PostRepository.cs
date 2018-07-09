using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Drivers;
using Blog.Domain.FunctionalCore;

namespace Blog.Infrastructure
{
    public class PostRepository : IPostRepository
    {
        private List<Post> _posts;

        public Post GetById(Guid postId)
        {
            return _posts.Single(post => post.GetPostId() == postId);
        }

        public void Add(Post post)
        {
            _posts.Add(post);
        }

        public void Save()
        {
            // do nothing, as posts are stored in memory
        }

        public PostRepository()
        {
            _posts = new List<Post>();
        }
    }
}
