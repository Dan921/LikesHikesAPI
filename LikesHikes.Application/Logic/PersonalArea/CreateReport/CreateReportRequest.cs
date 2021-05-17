using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.CreateReport
{
    public class CreateReportRequest : IRequest
    {
        public Guid? AppUserId { get; set; }

        public Guid RouteId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
    }
}
