using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Models
{
    public class BlogPostShortModel
    {
        public BlogPostShortModel(BlogPost post)
        {
            Id = post.Id;
            Heading = post.Heading;
            Text = post.Text;
            PublishingDate = post.PublishingDate.ToString("dd.MM.yyyy");
            AppImageId = post.AppImageId;
        }

        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public string PublishingDate { get; set; }

        public Guid? AppImageId { get; set; }
    }
}
