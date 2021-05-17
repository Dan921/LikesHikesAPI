using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.RemoveBlogPost
{
    public class RemoveBlogPostRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
