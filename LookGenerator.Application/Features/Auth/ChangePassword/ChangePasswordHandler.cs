using Application.Abstractions;


namespace Application.Features.Auth.ChangePassword ;

    public class ChangePasswordHandler(IUserService userService):ICommandHandler<ChangePasswordCommand>
    {
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await userService.ChangePasswordAsync(request.UserId, request.OldPassword, request.NewPassword);
        }
    }