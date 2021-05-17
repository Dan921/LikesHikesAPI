using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Domain.Repositories
{
    public interface IBlogPostCommentRepository : IGenericRepository<BlogPostComment>
    {
        Task<IQueryable<BlogPostComment>> GetByPostId(Guid postId);
    }
}
