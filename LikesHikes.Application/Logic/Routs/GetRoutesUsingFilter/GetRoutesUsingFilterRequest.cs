using LikesHikes.Application.Models;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.GetRoutesUsingFilter
{
    public class GetRoutesUsingFilterRequest : IRequest<IEnumerable<RoutePublicModel>>
    {
        public Guid? AppUserId { get; set; }

        public RouteFilterModel RouteFilter { get; set; }
    }
}
