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
            if(request.AppUserId != null)
            {
                var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                    .FirstOrDefault(p => p.AppUserId == request.AppUserId &&
                    p.RouteId == request.RouteId);

                if (userRoute != null)
                {
                    return new RouteDetailModel(userRoute.Route);
                }
            }
            else
            {
                var route = await unitOfWork.RouteRepository.GetById(request.RouteId);

                if (route != null && route.IsPublished)
                {
                    return new RouteDetailModel(route);
                }
            }

            throw new RestException("Маршрут не найден или недоступен");
        }
    }
}
