using Application.Exceptions;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Images.Posts.GetImage
{
    class GetImageQuery : IRequestHandler<GetImageRequest, byte[]>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetImageQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Handle(GetImageRequest request, CancellationToken cancellationToken)
        {
            var image = await unitOfWork.ImageRepository.GetById(request.Id);

            if (image != null)
            {
                return image.ImageByteArray;
            }

            throw new RestException("Изображение не найдено");
        }
    }
}
