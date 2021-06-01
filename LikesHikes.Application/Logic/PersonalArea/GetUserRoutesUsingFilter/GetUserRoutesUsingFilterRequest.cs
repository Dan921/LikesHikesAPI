using LikesHikes.Application.Models;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.GetUserRoutesUsingFilter
{
    public class GetUserRoutesUsingFilterRequest : IRequest<IEnumerable<RoutePrivateModel>>
    {
        public Guid? AppUserId { get; set; }

        public UserRouteFilterModel Filter { get; set; }
    }
}
