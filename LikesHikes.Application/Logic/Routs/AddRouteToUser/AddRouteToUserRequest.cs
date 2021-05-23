using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.AddRouteToUser
{
    public class AddRouteToUserRequest : IRequest
    {
        public Guid RouteId { get; set; }

        public Guid? AppUserId { get; set; }
    }
}
