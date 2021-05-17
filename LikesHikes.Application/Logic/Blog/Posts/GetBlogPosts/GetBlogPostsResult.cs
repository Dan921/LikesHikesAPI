using LikesHikes.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Blog.GetBlogPosts
{
    public class GetBlogPostsResult
    {
        public GetBlogPostsResult(IEnumerable<BlogPostShortModel> posts, PageModel pageModel)
        {
            Posts = posts;
            PageModel = pageModel;
        }

        public IEnumerable<BlogPostShortModel> Posts { get; }
        public PageModel PageModel { get; }
    }
}
