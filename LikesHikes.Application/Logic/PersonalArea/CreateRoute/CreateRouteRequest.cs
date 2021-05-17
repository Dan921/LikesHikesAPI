using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.CreateRout
{
    public class CreateRouteRequest : IRequest
    {
        public string Name { get; set; }

        public int Duration { get; set; }

        public Complexity Complexity { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public string KeyPoints { get; set; }

        public List<Coordinate> Coordinates { get; set; }

        public Guid? CreatedById { get; set; }
    }
}
