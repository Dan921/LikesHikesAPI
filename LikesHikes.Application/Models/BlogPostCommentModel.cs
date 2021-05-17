using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Models
{
    public class BlogPostCommentModel
    {
        public BlogPostCommentModel(BlogPostComment comment)
        {
            Id = comment.Id;
            UserName = comment.AppUser.UserName;
            Text = comment.Text;
            Time = comment.Time.ToString();
        }

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public string Time { get; set; }
    }
}
