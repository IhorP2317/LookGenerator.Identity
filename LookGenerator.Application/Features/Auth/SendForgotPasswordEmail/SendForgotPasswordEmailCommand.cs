using Application.Abstractions;

namespace Application.Features.Auth.SendForgotPasswordEmail ;

    public record SendForgotPasswordEmailCommand( string Email ):ICommand;