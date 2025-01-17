using Application.Abstractions;

namespace Application.Features.User_Management.DeleteUser ;

    public class DeleteUserHandler(IUserService userService):ICommandHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.SingleAsync(request.UserId);
            await userService.DeleteUserAsync(request.UserId);
        }
    }