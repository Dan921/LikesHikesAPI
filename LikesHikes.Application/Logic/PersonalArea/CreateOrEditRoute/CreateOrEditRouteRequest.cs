using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.CreateOrEditRoute
{
    public class CreateOrEditRouteRequest : IRequest
    {
        public Guid? RouteId { get; set; }

        public string RouteName { get; set; }

        public int Duration { get; set; }

        public string Complexity { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public string KeyPoints { get; set; }

        public List<Coordinate> Coordinates { get; set; }

        public Guid? AppUserId { get; set; }
    }
}
