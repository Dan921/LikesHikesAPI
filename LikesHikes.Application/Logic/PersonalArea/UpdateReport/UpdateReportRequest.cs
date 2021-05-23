using LikesHikes.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.UpdateReport
{
    public class UpdateReportRequest : IRequest<ReportModel>
    {
        public Guid RouteId { get; set; }

        public Guid? AppUserId { get; set; }

        public string ReportName { get; set; }

        public string Text { get; set; }
    }
}
