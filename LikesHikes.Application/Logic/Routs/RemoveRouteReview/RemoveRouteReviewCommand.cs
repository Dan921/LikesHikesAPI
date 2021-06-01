using Application.Exceptions;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.RemoveRouteReview
{
    public class RemoveRouteReviewCommand : IRequestHandler<RemoveRouteReviewRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveRouteReviewCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveRouteReviewRequest request, CancellationToken cancellationToken)
        {
            var review = await unitOfWork.RouteReviewRepository.GetById(request.Id);

            if (review == null)
            {
                throw new RestException("Отзыв не найден");
            }

            if(request.AppUserId == review.AppUserId || request.IsAdmin)
            {
                await unitOfWork.RouteReviewRepository.Remove(request.Id);

                var success = await unitOfWork.SaveAsync() > 0;

                if (success)
                {
                    var route = await unitOfWork.RouteRepository.GetById(review.RouteId);

                    if (route.CountOfVoces == 1)
                        route.Rating = null;
                    else
                        route.Rating = (route.Rating * route.CountOfVoces - review.Rating) / (route.CountOfVoces - 1);

                    route.CountOfVoces--;

                    await unitOfWork.RouteRepository.Update(route);

                    success = await unitOfWork.SaveAsync() > 0;

                    if (success)
                    {
                        return Unit.Value;
                    }

                    throw new RestException("Ошибка при обновлении маршрута. Отзыв удален");
                }

                throw new RestException("Ошибка при удалении отзыва");
            }

            throw new RestException("У вас нет прав на удаление");
        }
    }
}
