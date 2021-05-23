using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.GetUserRoutes
{
    public class GetUserRoutesQuery : IRequestHandler<GetUserRoutesRequest, IEnumerable<RoutePrivateModel>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserRoutesQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoutePrivateModel>> Handle(GetUserRoutesRequest request, CancellationToken cancellationToken)
        {
            var userRoutes = (await unitOfWork.UserRouteRepository.GetAll())
                    .Where(p => p.AppUserId == request.AppUserId);

            var routesModels = (await unitOfWork.RouteRepository.GetRoutesUsingFilter(request.RouteFilter))
                .Where(p => userRoutes.Select(p => p.Id).Contains(p.Id))
                .Select(p => new RoutePrivateModel(p));

            foreach (var route in routesModels)
            {
                if (userRoutes.FirstOrDefault(p => p.RouteId == route.Id).ReportId != null)
                    route.ReportExists = true;
            }

            return routesModels;
        }
    }
}
