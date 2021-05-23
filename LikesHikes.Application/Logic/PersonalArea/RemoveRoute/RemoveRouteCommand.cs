using Application.Exceptions;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.RemoveRoute
{
    public class RemoveRouteCommand : IRequestHandler<RemoveRouteRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveRouteCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveRouteRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId
                && p.RouteId == request.RouteId);

            if (userRoute != null)
            {
                var route = await unitOfWork.RouteRepository.GetById(request.RouteId);

                if (route == null)
                {
                    throw new RestException("Маршрут не найден");
                }

                if(userRoute.ReportId != null)
                {
                    await unitOfWork.ReportRepository.Remove((Guid)userRoute.ReportId);
                }

                await unitOfWork.UserRouteRepository.Remove(userRoute.Id);

                // Checking for the use of the route by other users and deleting if there are none

                var allUsersRoute = (await unitOfWork.UserRouteRepository.GetAll())
                    .Where(p => p.RouteId == request.RouteId);

                if (!allUsersRoute.Any())
                {
                    await unitOfWork.RouteRepository.Remove(request.RouteId);
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

            throw new Exception();
        }
    }
}
