using Application.Abstractions;
using Application.Models;

namespace Application.Features.Auth.SendEmailConfirmation ;

    public class SendEmailConfirmationHandler (IUserService userService, IEmailService emailService):ICommandHandler<SendEmailConfirmationCommand>
    {
        public async Task Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.SingleAsync(request.Email);
            var emailConfirmationToken = userService.GenerateEmailConfirmationTokenAsync(user.Email);
            if(user.EmailConfirmed) return;
            await emailService.SendEmailAsync(new SendEmailOptions
            {
                To = request.Email,
                Subject = "Confirm Your Account",
                TemplateFileName  = "AccountConfirmation.cshtml", 
                Model = new ConfirmAccountEmailModel
                {
                    UserName = user.UserName,
                    ConfirmationLink = $"https://localhost:7046/api/user/email/confirm?Email={request.Email}&ConfirmationToken={emailConfirmationToken}"
                }
            }, cancellationToken);
        }
    }