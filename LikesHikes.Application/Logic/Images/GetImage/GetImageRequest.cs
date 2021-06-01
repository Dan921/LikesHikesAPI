using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Images.Posts.GetImage
{
    public class GetImageRequest : IRequest<byte[]>
    {
        public Guid Id { get; set; }
    }
}
