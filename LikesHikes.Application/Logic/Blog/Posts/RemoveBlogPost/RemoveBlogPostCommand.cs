using Application.Exceptions;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.RemoveBlogPost
{
    public class RemoveBlogPostCommand : IRequestHandler<RemoveBlogPostRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveBlogPostCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveBlogPostRequest request, CancellationToken cancellationToken)
        {
            var post = await unitOfWork.BlogPostRepository.GetById(request.Id);

            if (post == null)
            {
                throw new RestException("Пост не найден");
            }

            var postCommnets = (await unitOfWork.BlogPostCommentRepository.GetAll()).Where(p => p.BlogPostId == request.Id);

            await unitOfWork.BlogPostCommentRepository.DeleteRange(postCommnets);

            await unitOfWork.BlogPostRepository.Remove(request.Id);

            if (post.AppImageId != null)
            {
                await unitOfWork.ImageRepository.Remove((Guid)post.AppImageId);
            }

            var success = await unitOfWork.SaveAsync() > 0;

            if (success)
            {
                return Unit.Value;
            }

            throw new Exception();
        }
    }
}
