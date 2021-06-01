using LikesHikes.Application.Models;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.GetUserRoutes
{
    public class GetUserRoutesRequest : IRequest<IEnumerable<RoutePrivateModel>>
    {
        public Guid? AppUserId { get; set; }
    }
}
