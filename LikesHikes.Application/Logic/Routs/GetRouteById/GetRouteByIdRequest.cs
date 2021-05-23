using LikesHikes.Application.Models;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.GetRouteById
{
    public class GetRouteByIdRequest : IRequest<RouteDetailModel>
    {
        public Guid? AppUserId { get; set; }

        public Guid RouteId { get; set; }
    }
}
