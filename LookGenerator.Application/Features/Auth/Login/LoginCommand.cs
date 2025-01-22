using Application.Abstractions;
using Application.Common.DTOs;

namespace Application.Features.Auth.Login ;

    public record LoginCommand( string Email, string Password):ICommand<TokenDto>;