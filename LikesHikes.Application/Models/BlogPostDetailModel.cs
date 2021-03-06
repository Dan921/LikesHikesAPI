using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LikesHikes.Application.Models
{
    public class BlogPostDetailModel
    {
        public BlogPostDetailModel(BlogPost post)
        {
            Id = post.Id;
            Heading = post.Heading;
            Text = post.Text;
            PublishingDate = post.PublishingDate.ToString("dd/MM/yyyy");
            Comments = post.BlogPostComments.Select(p => new BlogPostCommentModel(p));
            AppImagesId = post.AppImageId;
        }

        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public string PublishingDate { get; set; }

        public Guid? AppImagesId { get; set; }

        public IEnumerable<BlogPostCommentModel> Comments { get; set; }
    }
}
