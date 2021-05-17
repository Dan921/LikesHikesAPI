using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.RemoveReport
{
    public class RemoveReportCommand : IRequestHandler<RemoveReportRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveReportCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveReportRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId
                && p.RouteId == request.RouteId);

            if (userRoute != null)
            {
                if (userRoute.ReportId != request.ReportId)
                {
                    throw new ApplicationException("The route does not have such a report");
                }

                await unitOfWork.ReportRepository.Remove(request.ReportId);

                userRoute.Report = null;

                var success = await unitOfWork.SaveAsync() > 0;
                if (success)
                {
                    return Unit.Value;
                }
            }
            else
            {
                throw new ApplicationException("The user does not have this route");
            }
            throw new ApplicationException("Some problem");
        }
    }
}
