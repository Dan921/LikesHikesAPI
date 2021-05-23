using FluentValidation;

namespace LikesHikes.Application.Logic.Account.Registration
{
	public class RegistrationValidation : AbstractValidator<RegistrationRequest>
	{
		public RegistrationValidation()
		{
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}