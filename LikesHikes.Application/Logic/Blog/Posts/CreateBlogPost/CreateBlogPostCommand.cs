using Data.DAL;
using LikesHikes.Application.Helpers;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.CreateBlogPost
{
    public class CreateBlogPostCommand : IRequestHandler<CreateBlogPostRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateBlogPostCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateBlogPostRequest request, CancellationToken cancellationToken)
        {
            var blogPost = new BlogPost()
            {
                Heading = request.Heading,
                Text = request.Text,
                PublishingDate = DateTime.Now,
            };

            await unitOfWork.BlogPostRepository.Create(blogPost);

            var success = await unitOfWork.SaveAsync() > 0;

            if (success)
            {
                return Unit.Value;
            }

            throw new Exception();
        }
    }
}
