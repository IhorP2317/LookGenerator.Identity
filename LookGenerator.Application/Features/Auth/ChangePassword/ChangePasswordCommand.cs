using Application.Abstractions;

namespace Application.Features.Auth.ChangePassword ;

    public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword  ):ICommand;