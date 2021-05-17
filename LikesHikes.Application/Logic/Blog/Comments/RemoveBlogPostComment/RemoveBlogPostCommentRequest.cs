using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.RemoveBlogPostComment
{
    public class RemoveBlogPostCommentRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
