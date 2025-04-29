using Application.Abstractions;
using Application.Models;

namespace Application.Features.Auth.SendForgotPasswordEmail ;

    public class SendForgotPasswordEmailHandler(IUserService userService, IEmailService emailService):ICommandHandler<SendForgotPasswordEmailCommand>
    {
        public async Task Handle(SendForgotPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.SingleAsync(request.Email);
            var passwordResetToken = await userService.GeneratePasswordResetTokenAsync(user.Email);
            await emailService.SendEmailAsync(new SendEmailOptions
            {
                To = request.Email,
                Subject = "Forgot password",
                TemplateFileName = "ForgotPassword.cshtml",
                Model = new SendForgotPasswordEmailModel
                {
                    UserName = user.UserName,
                    ResetPasswordLink = $"http://localhost:4200/password/reset?email={request.Email}&token={passwordResetToken}"
                }
                
            }, cancellationToken);
        }
    }