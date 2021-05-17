using LikesHikes.Application.Models;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.UpdateBlogPost
{
    public class UpdateBlogPostCommand : IRequestHandler<UpdateBlogPostRequest, BlogPostDetailModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateBlogPostCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BlogPostDetailModel> Handle(UpdateBlogPostRequest request, CancellationToken cancellationToken)
        {
            var post = await unitOfWork.BlogPostRepository.GetById(request.Id);
            if (post == null)
            {
                throw new ApplicationException("Could not find post");
            }

            post.Heading = request.Heading;
            post.Text = request.Text;

            await unitOfWork.BlogPostRepository.Update(post);
            var success = await unitOfWork.SaveAsync() > 0;
            if (success)
            {
                return new BlogPostDetailModel(post);
            }
            throw new Exception("Some problem");
        }
    }
}
