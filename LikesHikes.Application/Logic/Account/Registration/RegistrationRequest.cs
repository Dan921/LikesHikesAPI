using LikesHikes.Application.Models;
using MediatR;

namespace LikesHikes.Application.Logic.User.Registration
{
	public class RegistrationRequest : IRequest<UserAuthModel>
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}