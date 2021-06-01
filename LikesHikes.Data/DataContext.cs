using LikesHikes.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<BlogPost> BlogPosts { get; protected set; }
        public DbSet<BlogPostComment> BlogPostComments { get; protected set; }
        public DbSet<Report> Reports { get; protected set; }
        public DbSet<Route> Routes { get; protected set; }
        public DbSet<RouteReview> RouteReviews { get; protected set; }
        public DbSet<UserRoute> UsersRoutes { get; protected set; }
        public DbSet<AppImage> Images { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

}
