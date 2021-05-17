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
    public class GetAllRoutesQuery : IRequestHandler<GetAllRoutesRequest, IEnumerable<RouteShortModel>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllRoutesQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RouteShortModel>> Handle(GetAllRoutesRequest request, CancellationToken cancellationToken)
        {
            var allRoutesModels = (await unitOfWork.RouteRepository.GetRoutesUsingFilter(request.RouteFilter))
                .Where(p => p.IsPublished)
                .Select(p => new RouteShortModel(p));

            return allRoutesModels;
        }
    }
}