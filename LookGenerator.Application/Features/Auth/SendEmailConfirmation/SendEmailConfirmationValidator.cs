using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.SendEmailConfirmation ;

    public class SendEmailConfirmationValidator:AbstractValidator<SendEmailConfirmationCommand>
    {
        public SendEmailConfirmationValidator(IUserService userService)
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, _) => await userService.IsExistAsync(email)).WithMessage(
                    "User is not exist!");
        }
    }