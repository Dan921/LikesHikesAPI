using LikesHikes.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.GetRouteById
{
    public class GetRouteByIdRequest : IRequest<RouteDetailModel>
    {
        public Guid Id { get; set; }
    }
}
