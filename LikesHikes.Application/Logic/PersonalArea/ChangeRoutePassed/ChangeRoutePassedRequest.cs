using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.MarkRoutePassed
{
    public class ChangeRoutePassedRequest : IRequest
    {
        public Guid RouteId { get; set; }

        public Guid? AppUserId { get; set; }
    }
}
