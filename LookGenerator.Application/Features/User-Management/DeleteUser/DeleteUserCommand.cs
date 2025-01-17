using Application.Abstractions;

namespace Application.Features.User_Management.DeleteUser ;

    public record DeleteUserCommand(Guid UserId):ICommand;