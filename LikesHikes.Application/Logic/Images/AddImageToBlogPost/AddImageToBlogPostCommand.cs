using LikesHikes.Application.Helpers;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Images.Posts.AddImageToBlogPost
{
    public class AddImageToBlogPostCommand : IRequestHandler<AddImageToBlogPostRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddImageToBlogPostCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddImageToBlogPostRequest request, CancellationToken cancellationToken)
        {
            var post = await unitOfWork.BlogPostRepository.GetById(request.BlogPostId);

            if (request.Image != null)
            {
                var image = new AppImage()
                {
                    ImageByteArray = ImageConverter.ConvertToByteArray(request.Image)
                };

                await unitOfWork.ImageRepository.Create(image);

                post.AppImage = image;

                await unitOfWork.BlogPostRepository.Update(post);
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
