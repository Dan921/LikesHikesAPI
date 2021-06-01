using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.CreateOrEditReport
{
    public class CreateOrEditReportRequest : IRequest
    {
        public Guid? AppUserId { get; set; }

        public Guid RouteId { get; set; }

        public string ReportName { get; set; }

        public string Text { get; set; }
    }
}
