using Application.Abstractions;
using Application.DTOs;

namespace Application.Features.Auth.RefreshAccessToken ;

    public record RefreshTokenCommand(TokenDto Token) : ICommand<TokenDto>;
