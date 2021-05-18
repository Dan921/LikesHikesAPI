using LikesHikes.Domain;
using MediatR;
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
                throw new ApplicationException("Could not find review");
            }
            await unitOfWork.RouteReviewRepository.Remove(request.Id);

            var success = await unitOfWork.SaveAsync() > 0;
            if (success)
            {
                return Unit.Value;
            }

            throw new ApplicationException("Some problem");
        }
    }
}
