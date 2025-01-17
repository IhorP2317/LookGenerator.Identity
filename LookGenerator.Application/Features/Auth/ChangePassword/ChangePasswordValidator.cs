using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.ChangePassword ;

    public class ChangePasswordValidator:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator(IUserService userService)
        {
            RuleFor(r => r.UserId)
                .NotEmpty()
                .MustAsync(async (userId, _) => await userService.IsExistAsync(userId)).WithMessage(
                    "User is not exist!");
            RuleFor(r => r.OldPassword)
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