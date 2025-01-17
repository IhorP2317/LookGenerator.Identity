using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.SendForgotPasswordEmail ;

    public class SendForgotPasswordEmailValidator : AbstractValidator<SendForgotPasswordEmailCommand>
    {
        public SendForgotPasswordEmailValidator(IUserService userService)
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, _) => await userService.IsEmailConfirmedAsync(email)).WithMessage(
                    "Email is not confirmed or not exist!");
        }
    }