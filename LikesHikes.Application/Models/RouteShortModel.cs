using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace LikesHikes.Application.Models
{
    public class RouteShortModel
    {
        public RouteShortModel(Route route)
        {
            Id = route.Id;
            Name = route.Name;
            Length = route.Length;
            Duration = route.Duration;
            Complexity = route.Complexity.ToString();
            Region = route.Region;
            KeyPoints = route.KeyPoints;
            Coordinates = JsonSerializer.Deserialize<List<Coordinate>>(route.Coordinates);
            Rating = route.Rating;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public int Duration { get; set; }

        public string Complexity { get; set; }

        public string Region { get; set; }

        public string KeyPoints { get; set; }

        public List<Coordinate> Coordinates { get; set; }

        public float Rating { get; set; }
    }
}
