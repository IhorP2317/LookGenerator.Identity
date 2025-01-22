using Application.Common.DTOs;
using Application.Features.Auth.Login;
using FastEndpoints;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class LoginEndpoint(ISender sender) : Endpoint<LoginCommand, TokenDto>
    {
        public override void Configure()
        {
            Post("/api/user/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            await SendAsync(await sender.Send(loginCommand, cancellationToken), cancellation: cancellationToken);
        }
    }