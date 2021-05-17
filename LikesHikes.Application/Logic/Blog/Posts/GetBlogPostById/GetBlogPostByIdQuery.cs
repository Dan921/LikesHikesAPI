using Data.DAL;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.GetBlogPostById
{
    public class GetBlogPostByIdQuery : IRequestHandler<GetBlogPostByIdRequest, BlogPostDetailModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetBlogPostByIdQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BlogPostDetailModel> Handle(GetBlogPostByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = await unitOfWork.BlogPostRepository.GetById(request.Id);
            if (blogPost == null)
            {
                throw new ApplicationException("Could not find post");
            }
            return new BlogPostDetailModel(blogPost);
        }
    }
}
