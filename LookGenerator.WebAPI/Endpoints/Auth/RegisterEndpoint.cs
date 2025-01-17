using Application.Features.Auth;
using FastEndpoints;
using LookGenerator.WebAPI.Dtos;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class RegisterEndpoint(ISender sender) : Endpoint<RegisterDto, RegisterResponse>
    {
        public override void Configure()
        {
            Post("/api/user/register");
            AllowAnonymous(); 
        }

        public override async Task HandleAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var registerCommand = new RegisterCommand(
                registerDto.UserName,
                registerDto.Email,
                registerDto.Password
                );

            await SendAsync(await sender.Send(registerCommand, cancellationToken), cancellation: cancellationToken);
        }
    }