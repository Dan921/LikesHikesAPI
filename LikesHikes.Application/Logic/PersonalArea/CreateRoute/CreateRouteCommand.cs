using LikesHikes.Application.Helpers;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.CreateRout
{
    public class CreateRouteCommand : IRequestHandler<CreateRouteRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private DistantHelper distantHelper = new DistantHelper();

        public CreateRouteCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateRouteRequest request, CancellationToken cancellationToken)
        {
            var route = new Route()
            {
                Name = request.Name,
                Length = distantHelper.CalculateDistant(request.Coordinates),
                Duration = request.Duration,
                Complexity = request.Complexity,
                Region = request.Region,
                Description = request.Description,
                KeyPoints = request.KeyPoints,
                Coordinates = JsonSerializer.Serialize<List<Coordinate>>(request.Coordinates),
                CreatedById = (Guid)request.CreatedById
            };

            await unitOfWork.RouteRepository.Create(route);

            var userRoute = new UserRoute()
            {
                AppUserId = (Guid)request.CreatedById,
                RouteId = route.Id
            };

            await unitOfWork.UserRouteRepository.Create(userRoute);
            var success = await unitOfWork.SaveAsync() > 0;
            if (success)
            {
                return Unit.Value;
            }
            throw new Exception("Some problem");
        }
    }
}
