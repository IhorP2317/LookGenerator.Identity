using Application.Features.Auth.ChangePassword;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class ChangePasswordEndpoint(ISender sender):Endpoint<ChangePasswordCommand>
    {
        public override void Configure()
        {
            Patch("/api/user/password/change");
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        }

        public override async Task HandleAsync(ChangePasswordCommand changePasswordCommand,
            CancellationToken cancellationToken)
        {
            await sender.Send(changePasswordCommand, cancellationToken);
            await SendNoContentAsync(cancellationToken);
        }
    }