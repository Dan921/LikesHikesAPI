using LikesHikes.Data;
using LikesHikes.Data.Repositories;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;

        public UnitOfWork(DataContext context)
        {        
            this.context = context;
        }

        public IGenericRepository<BlogPost> BlogPostRepository => new GenericRepository<BlogPost>(context);
        public IGenericRepository<Report> ReportRepository => new GenericRepository<Report>(context);
        public IRouteRepository RouteRepository => new RouteRepository(context);
        public IGenericRepository<RouteReview> RouteReviewRepository => new GenericRepository<RouteReview>(context);
        public IGenericRepository<UserRoute> UserRouteRepository => new GenericRepository<UserRoute>(context);
        public IGenericRepository<BlogPostComment> BlogPostCommentRepository => new GenericRepository<BlogPostComment>(context);

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
