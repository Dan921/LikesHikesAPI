using LikesHikes.Application.Models;
using MediatR;

namespace LikesHikes.Application.Logic.User.Login
{
    public class LoginRequest : IRequest<UserAuthModel>
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
