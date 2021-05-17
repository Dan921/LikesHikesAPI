using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.CreateBlogPostComment
{
    public class CreateBlogPostCommentRequest : IRequest
    {
        public Guid? UserId { get; set; }

        public Guid PostId { get; set; }

        public string Text { get; set; }
    }
}
