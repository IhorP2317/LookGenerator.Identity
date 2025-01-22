using Application.Abstractions;
using Application.Common.DTOs;

namespace Application.Features.Auth.RefreshAccessToken ;

    public class RefreshAccessTokenHandler(IUserService userService) : ICommandHandler<RefreshTokenCommand, TokenDto>
    {
        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            => await userService.RefreshAccessTokenAsync(request.Token);
    }