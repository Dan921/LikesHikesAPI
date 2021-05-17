using LikesHikes.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Routs.RemoveRout
{
    public class RemoveRoutCommand : IRequestHandler<RemoveRoutRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveRoutCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveRoutRequest request, CancellationToken cancellationToken)
        {
            var route = await unitOfWork.RouteRepository.GetById(request.Id);
            if (route == null)
            {
                throw new Exception("Could not find route");
            }
            await unitOfWork.RouteRepository.Remove(request.Id);

            var success = await unitOfWork.SaveAsync() > 0;
            if (success)
            {
                return Unit.Value;
            }

            throw new Exception("Some problem"); 
        }
    }
}
