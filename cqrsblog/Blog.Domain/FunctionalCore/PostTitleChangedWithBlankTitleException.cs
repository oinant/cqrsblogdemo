using System;

namespace Blog.Domain.FunctionalCore
{
    public class PostTitleChangedWithBlankTitleException : Exception
    {
        public PostTitleChangedWithBlankTitleException(Post @by, string changedBy)
            : base($"Post title was attempted to be changed with blank title by {changedBy}")
        {

        }
    }
}