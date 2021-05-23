using Application.Exceptions;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.AddRouteToUser
{
    public class AddRouteToUserCommand : IRequestHandler<AddRouteToUserRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddRouteToUserCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddRouteToUserRequest request, CancellationToken cancellationToken)
        {
            var route = await unitOfWork.RouteRepository.GetById(request.RouteId);

            if (route == null || !route.IsPublished)
            {
                throw new RestException("Маршрут не найден");
            }

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

            throw new Exception();
        }
    }
}
