using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Images.Posts.AddImageToBlogPost
{
    public class AddImageToBlogPostRequest : IRequest
    {
        public Guid BlogPostId { get; set; }

        public IFormFile Image { get; set; }
    }
}
