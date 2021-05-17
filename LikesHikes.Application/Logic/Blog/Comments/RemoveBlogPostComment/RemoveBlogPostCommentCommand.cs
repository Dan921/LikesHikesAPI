using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Blog.RemoveBlogPostComment
{
    public class RemoveBlogPostCommentCommand : IRequestHandler<RemoveBlogPostCommentRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveBlogPostCommentCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveBlogPostCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await unitOfWork.BlogPostCommentRepository.GetById(request.Id);
            if (comment == null)
            {
                throw new ApplicationException("Could not find comment");
            }
            await unitOfWork.BlogPostCommentRepository.Remove(request.Id);

            var success = await unitOfWork.SaveAsync() > 0;
            if (success)
            {
                return Unit.Value;
            }

            throw new Exception("Some problem");
        }
    }
}
