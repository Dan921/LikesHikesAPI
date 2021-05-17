using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Models
{
    public class RouteReviewModel
    {
        public RouteReviewModel(RouteReview route)
        {
            Id = route.Id;
            Text = route.Text;
            Rating = route.Rating;
            Time = route.Time.ToString();
            AppUserId = route.AppUserId;
            RouteId = route.RouteId;
        }

        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }

        public string Time { get; set; }

        public Guid AppUserId { get; set; }

        public Guid RouteId { get; set; }
    }
}
