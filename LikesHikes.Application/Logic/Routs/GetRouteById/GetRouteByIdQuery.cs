using LikesHikes.Application.Models;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.GetRouteById
{
    public class GetRouteByIdQuery : IRequestHandler<GetRouteByIdRequest, RouteDetailModel>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetRouteByIdQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<RouteDetailModel> Handle(GetRouteByIdRequest request, CancellationToken cancellationToken)
        {
            var route1 = (await unitOfWork.RouteRepository.GetRoutesUsingFilter(null)).FirstOrDefault(p => p.Id == request.Id);
            var route = await unitOfWork.RouteRepository.GetById(request.Id);

            if (route == null || !route.IsPublished)
            {
                throw new ApplicationException("Could not find post");
            }
            return new RouteDetailModel(route);
        }
    }
}
