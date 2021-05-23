using Application.Exceptions;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.CreateBlogPostComment
{
    public class CreateBlogPostCommentCommand : IRequestHandler<CreateBlogPostCommentRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateBlogPostCommentCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateBlogPostCommentRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId != null)
            {
                var blogPostComment = new BlogPostComment()
                {
                    BlogPostId = request.PostId,
                    AppUserId = (Guid)request.UserId,
                    Text = request.Text,
                    Time = DateTime.Now
                };

                await unitOfWork.BlogPostCommentRepository.Create(blogPostComment);

                var success = await unitOfWork.SaveAsync() > 0;

                if (success)
                {
                    return Unit.Value;
                }

                throw new Exception();
            }

            throw new RestException("Пользователь не найден");
        }
    }
}
