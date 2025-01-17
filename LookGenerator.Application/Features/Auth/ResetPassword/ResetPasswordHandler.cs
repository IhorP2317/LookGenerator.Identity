using Application.Abstractions;

namespace Application.Features.Auth.ResetPassword ;

    public class ResetPasswordHandler(IUserService userService) : ICommandHandler<ResetPasswordCommand>
    {
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var preparedToken = request.PasswordResetToken.Replace(' ', '+');
            await userService.ResetPasswordAsync(request.Email, preparedToken, request.NewPassword);
        }
    }