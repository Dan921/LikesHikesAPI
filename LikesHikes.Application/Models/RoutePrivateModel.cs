using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace LikesHikes.Application.Models
{
    public class RoutePrivateModel
    {
        public RoutePrivateModel(Route route)
        {
            Id = route.Id;
            Name = route.Name;
            Length = route.Length;
            Duration = route.Duration;

            if (route.Description.Length > 100)
                Description = route.Description.Substring(0, 100) + "...";
            else
                Description = route.Description;

            Complexity = route.Complexity.ToString();
            Region = route.Region;
            KeyPoints = route.KeyPoints;
            Coordinates = JsonSerializer.Deserialize<List<Coordinate>>(route.Coordinates);
            Rating = route.Rating;
            IsPublished = route.IsPublished;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public string Complexity { get; set; }

        public string Region { get; set; }

        public string KeyPoints { get; set; }

        public List<Coordinate> Coordinates { get; set; }

        public float Rating { get; set; }

        public bool ReportExists { get; set; }

        public bool IsPublished { get; set; }
    }
}
