using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.CreateRoutReview
{
    public class CreateRouteReviewRequest : IRequest
    {
        public string Text { get; set; }

        public int Rating { get; set; }

        public Guid? AppUserId { get; set; }

        public Guid RouteId { get; set; }
    }
}
