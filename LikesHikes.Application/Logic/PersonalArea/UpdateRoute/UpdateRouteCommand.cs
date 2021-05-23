using Application.Exceptions;
using LikesHikes.Application.Helpers;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.UpdateRout
{
    public class UpdateRouteCommand : IRequestHandler<UpdateRouteRequest, RouteDetailModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private DistantHelper distantHelper = new DistantHelper();

        public UpdateRouteCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<RouteDetailModel> Handle(UpdateRouteRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId
                && p.RouteId == request.Id);

            if (userRoute != null)
            {
                var route = await unitOfWork.RouteRepository.GetById(request.Id);

                if (route == null)
                {
                    throw new RestException("Маршрут не найден");
                }

                if (route.CreatedById == request.AppUserId)
                {
                    route.Name = request.RouteName;
                    route.Length = distantHelper.CalculateDistant(request.Coordinates);
                    route.Duration = request.Duration;
                    route.Complexity = request.Complexity;
                    route.Region = request.Region;
                    route.Description = request.Description;
                    route.KeyPoints = request.KeyPoints;
                    route.Coordinates = JsonSerializer.Serialize<List<Coordinate>>(request.Coordinates);

                    await unitOfWork.RouteRepository.Update(route);
                }
                else
                {
                    throw new RestException("Вы не являетесь создателем этого маршрута");
                }

                var success = await unitOfWork.SaveAsync() > 0;

                if (success)
                {
                    return new RouteDetailModel(route);
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
