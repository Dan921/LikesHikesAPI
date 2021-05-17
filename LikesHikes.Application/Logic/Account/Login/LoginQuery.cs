using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LikesHikes.Application.Models;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LikesHikes.Application.Logic.User.Login
{
    public class LoginQuery : IRequestHandler<LoginRequest, UserAuthModel>
	{
		private readonly UserManager<AppUser> _userManager;

		private readonly SignInManager<AppUser> _signInManager;

		private readonly IJwtGenerator _jwtGenerator;

		public LoginQuery(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<UserAuthModel> Handle(LoginRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				throw new Exception("User is not found");
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded)
			{
				return new UserAuthModel
				{
					Token = await _jwtGenerator.CreateToken(user),
					UserName = user.UserName
				};
			}
			else
				throw new Exception("Invalid password");

			throw new Exception(HttpStatusCode.Unauthorized.ToString());
		}
	}
}
