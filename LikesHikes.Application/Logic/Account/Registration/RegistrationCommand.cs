using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LikesHikes.Application.Models;
using LikesHikes.Data;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LikesHikes.Application.Logic.User.Registration
{
	public class RegistrationCommand : IRequestHandler<RegistrationRequest, UserAuthModel>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IJwtGenerator _jwtGenerator;
		private readonly DataContext _context;

		public RegistrationCommand(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
		{
			_context = context;
			_userManager = userManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<UserAuthModel> Handle(RegistrationRequest request, CancellationToken cancellationToken)
		{
			if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
			{
				throw new Exception("Email already exist");
			}

			if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
			{
				throw new Exception("UserName already exist");
			}

			var user = new AppUser
			{
				Email = request.Email,
				UserName = request.UserName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				return new UserAuthModel
				{
					Token = await _jwtGenerator.CreateToken(user),
					UserName = user.UserName,
				};
			}

			throw new Exception("Client creation failed");
		}
	}
}