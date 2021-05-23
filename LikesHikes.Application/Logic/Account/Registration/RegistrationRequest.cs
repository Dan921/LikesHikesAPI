using LikesHikes.Application.Models;
using MediatR;

namespace LikesHikes.Application.Logic.Account.Registration
{
	public class RegistrationRequest : IRequest
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
	}
}