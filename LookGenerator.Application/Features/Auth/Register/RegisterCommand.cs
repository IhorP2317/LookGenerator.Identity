using Application.Abstractions;

namespace Application.Features.Auth ;

    public sealed record RegisterCommand(string UserName, string Email, string Password, string Role = "User"):ICommand<RegisterResponse>;
