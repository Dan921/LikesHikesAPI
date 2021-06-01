using LikesHikes.Application.Models;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LikesHikes.Application.Logic.Account.GetUserData
{
    public class GetUserDataQuery : IRequestHandler<GetUserDataRequest, GetUserDataResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;

        public GetUserDataQuery(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<GetUserDataResult> Handle(GetUserDataRequest request, CancellationToken cancellationToken)
        {
            var userRoutes = (await unitOfWork.UserRouteRepository.GetAll())
                .Where(p => p.AppUserId == request.AppUserId);

            var routesCount = userRoutes.Count();

            var passedRoutesCount = userRoutes.Where(p => p.IsPassed).Count();

            var user = await userManager.FindByIdAsync(request.AppUserId.ToString());

            var userDataResult = new GetUserDataResult
            {
                Email = user.Email,
                UserName = user.UserName,
                IsAdmin = await userManager.IsInRoleAsync(user, nameof(UserRole.Admin)),
                RoutesCount = routesCount,
                PassedRoutesCount = passedRoutesCount
            };

            return userDataResult;
        }
    }
}
