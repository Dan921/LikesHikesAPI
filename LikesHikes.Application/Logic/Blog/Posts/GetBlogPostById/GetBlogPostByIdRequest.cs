using LikesHikes.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.GetBlogPostById
{
    public class GetBlogPostByIdRequest : IRequest<BlogPostDetailModel>
    {
        public Guid Id { get; set; }
    }
}
