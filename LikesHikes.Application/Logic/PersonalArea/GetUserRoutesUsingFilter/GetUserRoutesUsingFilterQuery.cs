using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.GetUserRoutesUsingFilter
{
    public class GetUserRoutesUsingFilterQuery : IRequestHandler<GetUserRoutesUsingFilterRequest, IEnumerable<RoutePrivateModel>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserRoutesUsingFilterQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoutePrivateModel>> Handle(GetUserRoutesUsingFilterRequest request, CancellationToken cancellationToken)
        {
            var userRoutes = (await unitOfWork.UserRouteRepository.GetAll())
                    .Where(p => p.AppUserId == request.AppUserId);

            var routesModels = (await unitOfWork.RouteRepository.GetUserRoutesUsingFilter(request.Filter))
                .Where(p => userRoutes.Select(p => p.RouteId).Contains(p.Id))
                .Select(p => new RoutePrivateModel(p))
                .ToList();

            foreach (var route in routesModels)
            {
                if (userRoutes.FirstOrDefault(p => p.RouteId == route.Id).ReportId != null)
                    route.ReportExists = true;
                if (userRoutes.FirstOrDefault(p => p.RouteId == route.Id).IsPassed == true)
                    route.IsPassed = true;
            }

            return routesModels;
        }
    }
}
