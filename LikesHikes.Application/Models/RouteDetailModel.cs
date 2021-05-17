using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace LikesHikes.Application.Models
{
    public class RouteDetailModel
    {
        public RouteDetailModel(Route route)
        {
            Id = route.Id;
            Name = route.Name;
            Length = route.Length;
            Duration = route.Duration;
            Complexity = route.Complexity.ToString();
            Region = route.Region;
            Description = route.Description;
            KeyPoints = route.KeyPoints;
            Coordinates = JsonSerializer.Deserialize<List<Coordinate>>(route.Coordinates);
            isPublished = route.IsPublished;
            Rating = route.Rating;
            CountOfVoces = route.CountOfVoces;
            AuthorName = route.CreatedBy.UserName;
            RouteReviewModels = route.RouteReviews.Select(p => new RouteReviewModel(p));
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public int Duration { get; set; }

        public string Complexity { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public string KeyPoints { get; set; }

        public List<Coordinate> Coordinates { get; set; }

        public bool isPublished { get; set; }

        public float Rating { get; set; }

        public int CountOfVoces { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<RouteReviewModel> RouteReviewModels { get; set; }
    }
}
