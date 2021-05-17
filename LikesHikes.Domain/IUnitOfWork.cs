using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Domain
{
    public interface IUnitOfWork
    {
        IGenericRepository<BlogPost> BlogPostRepository { get; }
        IGenericRepository<Report> ReportRepository { get; }
        IRouteRepository RouteRepository { get; }
        IGenericRepository<RouteReview> RouteReviewRepository { get; }
        IGenericRepository<UserRoute> UserRouteRepository { get; }
        IGenericRepository<BlogPostComment> BlogPostCommentRepository { get; }

        Task<int> SaveAsync();
    }
}
