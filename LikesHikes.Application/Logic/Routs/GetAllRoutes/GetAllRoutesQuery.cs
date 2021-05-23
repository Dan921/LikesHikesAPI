using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.GetRoutes
{
    public class GetAllRoutesQuery : IRequestHandler<GetAllRoutesRequest, IEnumerable<RoutePublicModel>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllRoutesQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoutePublicModel>> Handle(GetAllRoutesRequest request, CancellationToken cancellationToken)
        {
            var allRoutesModels = (await unitOfWork.RouteRepository.GetRoutesUsingFilter(request.RouteFilter))
                .Where(p => p.IsPublished)
                .Select(p => new RoutePublicModel(p));

            if (request.AppUserId != null)
            {
                var userRouteIds = (await unitOfWork.UserRouteRepository.GetAll())
                    .Where(p => p.AppUserId == request.AppUserId)
                    .Select(p => p.RouteId);

                foreach (var route in allRoutesModels)
                {
                    if (userRouteIds.Contains(route.Id))
                        route.UserHas = true;
                }
            }

            return allRoutesModels;
        }
    }
}