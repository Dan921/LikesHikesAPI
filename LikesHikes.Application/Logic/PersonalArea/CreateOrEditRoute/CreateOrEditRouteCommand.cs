using Application.Exceptions;
using LikesHikes.Application.Helpers;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.CreateOrEditRoute
{
    public class CreateOrEditRouteCommand : IRequestHandler<CreateOrEditRouteRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private DistantHelper distantHelper = new DistantHelper();

        public CreateOrEditRouteCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateOrEditRouteRequest request, CancellationToken cancellationToken)
        {
            if(request.RouteId != null)
            {
                var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId
                && p.RouteId == request.RouteId);

                if (userRoute != null)
                {
                    var route = await unitOfWork.RouteRepository.GetById((Guid)request.RouteId);

                    if (route == null)
                    {
                        throw new RestException("Маршрут не найден");
                    }

                    if (route.CreatedById == request.AppUserId)
                    {
                        route.Name = request.RouteName;
                        route.Duration = request.Duration;
                        route.Complexity = GetComplexity(request.Complexity);
                        route.Region = request.Region;
                        route.Description = request.Description;
                        route.KeyPoints = request.KeyPoints;

                        await unitOfWork.RouteRepository.Update(route);
                    }
                    else
                    {
                        throw new RestException("Вы не являетесь создателем этого маршрута");
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
            }
            else
            {
                var route = new Route()
                {
                    Name = request.RouteName,
                    Length = distantHelper.CalculateDistant(request.Coordinates),
                    Duration = request.Duration,
                    Complexity = GetComplexity(request.Complexity),
                    Region = request.Region,
                    Description = request.Description,
                    KeyPoints = request.KeyPoints,
                    Coordinates = JsonSerializer.Serialize<List<Coordinate>>(request.Coordinates),
                    CreatedById = (Guid)request.AppUserId
                };

                await unitOfWork.RouteRepository.Create(route);

                var userRoute = new UserRoute()
                {
                    AppUserId = (Guid)request.AppUserId,
                    RouteId = route.Id
                };

                await unitOfWork.UserRouteRepository.Create(userRoute);

                var success = await unitOfWork.SaveAsync() > 0;

                if (success)
                {
                    return Unit.Value;
                }
            }

            throw new Exception();
        }

        public Complexity GetComplexity(string complexityString)
        {
            switch (complexityString)
            {
                case "Легкий":
                    return Complexity.Easy;
                case "Сложный":
                    return Complexity.Difficult;
                default:
                    return Complexity.Medium;
            }
        }
    }
}
