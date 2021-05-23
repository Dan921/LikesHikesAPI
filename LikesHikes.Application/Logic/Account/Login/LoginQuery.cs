using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LikesHikes.Application.Logic.Account.Login
{
    public class LoginQuery : IRequestHandler<LoginRequest, LoginResult>
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IJwtGenerator _jwtGenerator;

		public LoginQuery(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
		{
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
			_signInManager = signInManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<LoginResult> Handle(LoginRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null)
            {
                throw new RestException("Пользователь не найден");
            }

			var userRoutes = (await unitOfWork.UserRouteRepository.GetAll())
				.Where(p => p.AppUserId == user.Id);

			var routesCount = userRoutes.Count();

			var passedRoutesCount = userRoutes.Where(p => p.IsPassed).Count();

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded)
			{
				return new LoginResult
				{
					Email = user.Email,
					UserName = user.UserName,
					IsAdmin = await _userManager.IsInRoleAsync(user, nameof(UserRole.Admin)),
					RoutesCount = routesCount,
					PassedRoutesCount = passedRoutesCount,
					Token = await _jwtGenerator.CreateToken(user)
				};
			}

			throw new RestException("Неправильный пароль");
		}
	}
}
