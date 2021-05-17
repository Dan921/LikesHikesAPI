using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.RemoveRoute
{
    public class RemoveRouteRequest : IRequest
    {
        public Guid RouteId { get; set; }

        public Guid? AppUserId { get; set; }
    }
}
