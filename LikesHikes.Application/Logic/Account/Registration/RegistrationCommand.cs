using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using LikesHikes.Application.Models;
using LikesHikes.Data;
using LikesHikes.Domain;
using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LikesHikes.Application.Logic.Account.Registration
{
	public class RegistrationCommand : IRequestHandler<RegistrationRequest>
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

		public async Task<Unit> Handle(RegistrationRequest request, CancellationToken cancellationToken)
		{
			if(request.Password != request.ConfirmPassword)
            {
				throw new RestException("Пароли не совпадают");
			}

			if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
			{
				throw new RestException("Email уже занят");
			}

			if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
			{
				throw new RestException("Уже существует пользователь с таким именем");
			}

			var user = new AppUser
			{
				Email = request.Email,
				UserName = request.UserName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			await _userManager.AddToRoleAsync(user, nameof(UserRole.User));

			if (result.Succeeded)
			{
				return Unit.Value;
			}

			throw new RestException("Некорректный пароль");
		}
	}
}