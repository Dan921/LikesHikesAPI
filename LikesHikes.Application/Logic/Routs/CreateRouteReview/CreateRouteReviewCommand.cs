using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.CreateRoutReview
{
    public class CreateRouteReviewCommand : IRequestHandler<CreateRouteReviewRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateRouteReviewCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateRouteReviewRequest request, CancellationToken cancellationToken)
        {
            var routeReview = new RouteReview()
            {
                Text = request.Text,
                Rating = request.Rating,
                Time = DateTime.Now,
                AppUserId = (Guid)request.AppUserId,
                RouteId = request.RouteId
            };

            var route = await unitOfWork.RouteRepository.GetById(routeReview.RouteId);

            route.Rating = (route.Rating * route.CountOfVoces + routeReview.Rating) / (route.CountOfVoces + 1);
            route.CountOfVoces++;

            await unitOfWork.RouteReviewRepository.Create(routeReview);
            var success = await unitOfWork.SaveAsync() > 0;
            if (success)
            {
                return Unit.Value;
            }
            throw new Exception("Some problem");
        }
    }
}
