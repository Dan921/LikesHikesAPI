using Application.Exceptions;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.PublishRoute
{
    public class PublishRouteCommand : IRequestHandler<PublishRouteRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public PublishRouteCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(PublishRouteRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId
                && p.RouteId == request.RouteId);

            if (userRoute != null)
            {
                var route = await unitOfWork.RouteRepository.GetById(request.RouteId);

                if (route.IsBanned)
                {
                    throw new RestException("Маршрут заблокирован администрацией");
                }

                route.IsPublished = true;

                await unitOfWork.RouteRepository.Update(route);

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
            throw new Exception();
        }
    }
}
