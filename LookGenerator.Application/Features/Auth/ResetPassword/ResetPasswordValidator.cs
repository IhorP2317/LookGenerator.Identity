using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.ResetPassword ;

    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator(IUserService userService)
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, _) => await userService.IsEmailConfirmedAsync(email)).WithMessage(
                    "Email is not confirmed or not exist!");
            RuleFor(r => r.PasswordResetToken)
                .NotEmpty();
            RuleFor(r => r.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+")
                .Matches(@"[^\w\s_]+|_");
        }
    }