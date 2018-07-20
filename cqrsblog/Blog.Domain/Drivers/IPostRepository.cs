using System;
using Blog.Domain.FunctionalCore;

namespace Blog.Domain.Drivers
{
    public interface IPostRepository
    {
        Post GetById(Guid postId);
        void Add(Post post);
        void Save(Post post);
    }
}
