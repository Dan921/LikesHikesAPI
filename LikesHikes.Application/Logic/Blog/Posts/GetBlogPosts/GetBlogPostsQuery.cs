using Data.DAL;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.GetBlogPosts
{
    public class GetBlogPostsQuery : IRequestHandler<GetBlogPostsRequest, GetBlogPostsResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetBlogPostsQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetBlogPostsResult> Handle(GetBlogPostsRequest request, CancellationToken cancellationToken)
        {
            var allBlogPosts = await unitOfWork.BlogPostRepository.GetAll();

            var count = allBlogPosts.Count();
            var items = allBlogPosts.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            var blogPosts = items.Select(p => new BlogPostShortModel(p));

            PageModel pageModel = new PageModel(count, request.Page, request.PageSize);

            return new GetBlogPostsResult(blogPosts, pageModel);
        }
    }
}
