using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth ;

    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator(IUserService userService)
        {
            RuleFor(rc => rc.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MustAsync(async (email, _) => (await userService.SingleOrDefaultAsync(email)) == null)
                .WithMessage("The email has already been used for another account.");

            RuleFor(rc => rc.UserName)
                .NotEmpty()
                .MustAsync(async (username, _) => await userService.IsUserNameUniqueAsync(username))
                .WithMessage("The username is already taken.");

         
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