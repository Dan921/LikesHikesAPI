using FluentValidation;

namespace LikesHikes.Application.Logic.User.Login
{
    public class LoginValidation : AbstractValidator<LoginRequest>
	{
		public LoginValidation()
		{
			RuleFor(x => x.Email).NotEmpty();

			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
