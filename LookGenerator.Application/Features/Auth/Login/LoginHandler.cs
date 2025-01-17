using Application.Abstractions;
using Application.DTOs;

namespace Application.Features.Auth.Login ;

    public class LoginHandler(IUserService userService ):ICommandHandler<LoginCommand, TokenDto>
    {
        public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userDto = await userService.SingleAsync(request.Email);
            userDto.Password = request.Password;
          return  await userService.LoginAsync(userDto);
        }
    }