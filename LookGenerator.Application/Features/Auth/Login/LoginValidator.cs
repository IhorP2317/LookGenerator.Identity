using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.Login ;

    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator(IUserService userService)
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, _) => await userService.IsEmailConfirmedAsync(email)).WithMessage(
                    "Email is not confirmed or not exist!");
            RuleFor(rc => rc.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+")
                .Matches(@"[^\w\s_]+|_");
        }
    }