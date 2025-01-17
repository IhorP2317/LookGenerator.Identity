using Application.Abstractions;
using Application.DTOs;

namespace Application.Features.Auth.Login ;

    public record LoginCommand( string Email, string Password):ICommand<TokenDto>;