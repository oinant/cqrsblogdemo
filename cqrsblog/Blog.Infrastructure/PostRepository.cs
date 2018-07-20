using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Drivers;
using Blog.Domain.FunctionalCore;

namespace Blog.Infrastructure
{
    public class PostRepository : IPostRepository
    {
        public Post GetById(Guid postId)
        {
            using (var context = new BlogContext())
            {
                return context.Posts.Find(postId);
            }
        }

        public void Add(Post post)
        {
            using (var context = new BlogContext())
            {
                context.Posts.Add(post);
                context.SaveChanges();
            }
        }

        public void Save(Post post)
        {
            using (var context = new BlogContext())
            {
                context.Entry<Post>(post);
                context.SaveChanges();
            }
        }

       
    }
}
