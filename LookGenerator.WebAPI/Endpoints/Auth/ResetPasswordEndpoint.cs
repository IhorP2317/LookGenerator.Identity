using Application.Features.Auth.ResetPassword;
using FastEndpoints;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class ResetPasswordEndpoint(ISender sender):Endpoint<ResetPasswordCommand>
    {
        public override void Configure()
        {
            Patch("/api/users/password/reset");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ResetPasswordCommand resetPasswordCommand,
            CancellationToken cancellationToken)
        {
            await sender.Send(resetPasswordCommand, cancellationToken);
            await SendNoContentAsync(cancellationToken);
        }
    }