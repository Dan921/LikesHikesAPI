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

namespace LikesHikes.Application.Logic.PersonalArea.UpdateReport
{
    public class UpdateReportCommand : IRequestHandler<UpdateReportRequest, ReportModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateReportCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ReportModel> Handle(UpdateReportRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId
                && p.RouteId == request.RouteId);

            if (userRoute != null)
            {

                if(userRoute.Report != null)
                {
                    throw new RestException("Такого отчета нет");
                }

                var report = await unitOfWork.ReportRepository.GetById((Guid)userRoute.ReportId);

                report.Name = request.ReportName;
                report.Text = request.Text;

                await unitOfWork.ReportRepository.Update(report);

                var success = await unitOfWork.SaveAsync() > 0;

                if (success)
                {
                    return new ReportModel(report);
                }
            }
            else
            {
                throw new RestException("У пользователя нет такого маршрута");
            }

            throw new Exception();
        }
    }
}
