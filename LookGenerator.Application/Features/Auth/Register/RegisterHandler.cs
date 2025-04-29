using Application.Abstractions;
using Application.Models;

namespace Application.Features.Auth ;

    public class RegisterHandler(IUserService userService,IEmailService emailService):ICommandHandler<RegisterCommand, RegisterResponse>
    {
        private readonly RegisterMapper _mapper = new();
        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
          
           var emailConfirmationToken = await userService.RegisterAsync(_mapper.ToUserDto(request));
            await emailService.SendEmailAsync(new SendEmailOptions
            {
                To = request.Email,
                Subject = "Confirm Your Account",
                TemplateFileName  = "AccountConfirmation.cshtml", 
                Model = new ConfirmAccountEmailModel
                {
                    UserName = request.UserName,
                    ConfirmationLink = $"http://localhost:4200/email/confirm?email={request.Email}&token={emailConfirmationToken}"
                }
            }, cancellationToken);
            var userDto =  await userService.SingleAsync(request.Email);
            return _mapper.ToRegisterResponse(userDto);
        }
    }