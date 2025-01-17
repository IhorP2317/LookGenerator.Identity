

using Application.Abstractions;

namespace Application.Features.Auth.ConfirmEmail ;

    public record ConfirmEmailCommand( string Email, string ConfirmationToken ):ICommand;