using Application.Exceptions;
using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.PersonalArea.MarkRoutePassed
{
    class ChangeRoutePassedCommand : IRequestHandler<ChangeRoutePassedRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public ChangeRoutePassedCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeRoutePassedRequest request, CancellationToken cancellationToken)
        {
            var userRoute = (await unitOfWork.UserRouteRepository.GetAll())
                .FirstOrDefault(p => p.AppUserId == request.AppUserId && 
                p.RouteId == request.RouteId);

            if (userRoute != null)
            {
                if (userRoute.IsPassed)
                {
                    throw new RestException("Маршрут уже пройден");
                }

                userRoute.IsPassed = true;

                await unitOfWork.UserRouteRepository.Update(userRoute);

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
