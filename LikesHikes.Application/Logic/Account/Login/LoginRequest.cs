using LikesHikes.Application.Models;
using MediatR;

namespace LikesHikes.Application.Logic.Account.Login
{
    public class LoginRequest : IRequest<LoginResult>
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
