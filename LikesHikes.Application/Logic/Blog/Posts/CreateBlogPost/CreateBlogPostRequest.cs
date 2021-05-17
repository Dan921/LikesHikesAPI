using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.CreateBlogPost
{
    public class CreateBlogPostRequest : IRequest
    {
        public string Heading { get; set; }

        public string Text { get; set; }
    }
}
