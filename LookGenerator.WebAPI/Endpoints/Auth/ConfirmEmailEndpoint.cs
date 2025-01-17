using Application.Features.Auth.ConfirmEmail;
using FastEndpoints;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class ConfirmEmailEndpoint(ISender sender) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            
            Post("/api/user/email/confirm");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            
            var email = Query<string>("Email");
            var confirmationToken = Query<string>("ConfirmationToken");
            var confirmEmailCommand = new ConfirmEmailCommand(email ?? "", confirmationToken ?? "");
            await sender.Send(confirmEmailCommand, cancellationToken);
            await SendNoContentAsync(cancellationToken);
        }
    }
