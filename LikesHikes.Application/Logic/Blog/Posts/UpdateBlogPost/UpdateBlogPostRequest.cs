using LikesHikes.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.UpdateBlogPost
{
    public class UpdateBlogPostRequest : IRequest<BlogPostDetailModel>
    {
        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }
    }
}
