using Application.Common.DTOs;
using Application.Features.Auth.RefreshAccessToken;
using FastEndpoints;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class RefreshAccessTokenEndpoint(ISender sender):Endpoint<RefreshTokenCommand,TokenDto>
    {
        public override void Configure()
        {
            Put("/api/user/token/refresh");
            AllowAnonymous();
        }
        public override async Task HandleAsync(RefreshTokenCommand refreshTokenCommand,CancellationToken cancellationToken)
        {
            await SendAsync(await sender.Send(refreshTokenCommand, cancellationToken), cancellation: cancellationToken);
        }
    }