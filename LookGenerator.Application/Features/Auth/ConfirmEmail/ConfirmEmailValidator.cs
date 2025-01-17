using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.ConfirmEmail ;

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailValidator(IUserService userService)
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, _) => (await userService.SingleOrDefaultAsync(email)) != null)
                .WithMessage("User does not exist.");
            RuleFor(c => c.ConfirmationToken).NotEmpty().WithMessage("Confirmation Token must not be empty.");
        }
    }