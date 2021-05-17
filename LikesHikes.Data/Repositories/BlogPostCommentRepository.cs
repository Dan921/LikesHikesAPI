using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Data
{
    public class BlogPostCommentRepository : GenericRepository<BlogPostComment>, IBlogPostCommentRepository
    {
        public BlogPostCommentRepository(DataContext context) : base(context)
        {
        }

        public Task<IQueryable<BlogPostComment>> GetByPostId(Guid postId)
        {
            IQueryable<BlogPostComment> data = dbSet.Where(p => p.BlogPost.Id == postId);
            return Task.FromResult(data);
        }
    }
}
