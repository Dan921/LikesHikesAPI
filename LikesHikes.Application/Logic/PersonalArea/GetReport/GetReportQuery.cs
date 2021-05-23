using Application.Exceptions;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.GetReport
{
    public class GetReportQuery : IRequestHandler<GetReportRequest, ReportModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetReportQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ReportModel> Handle(GetReportRequest request, CancellationToken cancellationToken)
        {
            var report = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId && 
                p.RouteId == request.RouteId).Report;
            
            if(report == null)
            {
                throw new RestException("Отчет не найден");
            }

            var reportModel = new ReportModel(report);

            return reportModel;
        }
    }
}
