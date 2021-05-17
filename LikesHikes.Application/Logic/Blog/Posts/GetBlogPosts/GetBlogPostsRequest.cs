using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.GetBlogPosts
{
    public class GetBlogPostsRequest : IRequest<GetBlogPostsResult>
    {
        public int PageSize { get; set; } = 12;

        public int Page { get; set; } = 1;
    }
}
