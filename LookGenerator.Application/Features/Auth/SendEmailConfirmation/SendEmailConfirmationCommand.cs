using Application.Abstractions;

namespace Application.Features.Auth.SendEmailConfirmation ;

    public record SendEmailConfirmationCommand(string Email ):ICommand;