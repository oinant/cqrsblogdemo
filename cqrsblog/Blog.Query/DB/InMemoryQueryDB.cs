using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Query.DB
{
    public class InMemoryQueryDB
    {
        internal List<PostSummaryDTO> Summaries { get; set; }
        internal List<PostDTO> Posts { get; set; }

        public InMemoryQueryDB()
        {
            Summaries = new List<PostSummaryDTO>();
            Posts = new List<PostDTO>();
        }

        public IReadOnlyList<PostSummaryDTO> GetAllPostSummaries()
        {
            return Summaries.AsReadOnly();
        }

        public PostDTO GetPost(Guid postId)
        {
            return Posts.Single(p => p.PostId == postId);
        }
    }

    public class PostDTO
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }

    public class PostSummaryDTO
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
    }
}