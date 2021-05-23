using Application.Exceptions;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
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

            await unitOfWork.BlogPostRepository.Remove(request.Id);

            var success = await unitOfWork.SaveAsync() > 0;

            if (success)
            {
                return Unit.Value;
            }

            throw new Exception();
        }
    }
}
