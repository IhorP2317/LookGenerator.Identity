using Application.Abstractions;

namespace Application.Features.Auth.ResetPassword ;

    public record ResetPasswordCommand(string Email, string PasswordResetToken,string NewPassword ):ICommand;