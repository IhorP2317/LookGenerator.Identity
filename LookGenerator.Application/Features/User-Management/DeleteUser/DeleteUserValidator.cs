using Application.Abstractions;
using FluentValidation;

namespace Application.Features.User_Management.DeleteUser ;

    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator(IUserService userService)
        {
            RuleFor(d => d.UserId)
                .NotEmpty()
                .MustAsync(async (userId, _) => await userService.IsExistAsync(userId)).WithMessage(
                    "User does not exist!");
        }
    }