using LikesHikes.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.GetReport
{
    public class GetReportRequest : IRequest<ReportModel>
    {
        public Guid? AppUserId { get; set; }

        public Guid RouteId { get; set; }
    }
}
