using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.RemoveRouteReview
{
    public class RemoveRouteReviewRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
