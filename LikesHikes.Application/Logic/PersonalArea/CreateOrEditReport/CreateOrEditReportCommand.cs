using Application.Exceptions;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.CreateOrEditReport
{
    public class CreateOrEditReportCommand : IRequestHandler<CreateOrEditReportRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateOrEditReportCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateOrEditReportRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId && 
                p.RouteId == request.RouteId);

            if (userRoute != null)
            {
                var report = userRoute.Report;

                if (userRoute.Report != null)
                {
                    report.Name = request.ReportName;
                    report.Text = request.Text;

                    await unitOfWork.ReportRepository.Update(report);
                }
                else
                {
                    report = new Report()
                    {
                        Name = request.ReportName,
                        Text = request.Text
                    };

                    await unitOfWork.ReportRepository.Create(report);

                    userRoute.ReportId = report.Id;

                    await unitOfWork.UserRouteRepository.Update(userRoute);
                }

                var success = await unitOfWork.SaveAsync() > 0;

                if (success)
                {
                    return Unit.Value;
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
