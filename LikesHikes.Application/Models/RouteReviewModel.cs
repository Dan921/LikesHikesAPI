using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Models
{
    public class RouteReviewModel
    {
        public RouteReviewModel(RouteReview review)
        {
            Id = review.Id;
            Text = review.Text;
            Rating = review.Rating;
            Time = review.Time.Date.ToString("dd/MM/yyyy");
            AppUserId = review.AppUserId;
            AuthorName = "Пользователь";
            RouteId = review.RouteId;
        }

        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }

        public string Time { get; set; }

        public string AuthorName { get; set; }

        public Guid AppUserId { get; set; }

        public Guid RouteId { get; set; }
    }
}
