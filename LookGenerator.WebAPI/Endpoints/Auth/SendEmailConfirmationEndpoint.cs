using Application.Features.Auth.SendEmailConfirmation;
using FastEndpoints;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class SendEmailConfirmationEndpoint(ISender sender):EndpointWithoutRequest
    {
        public override void Configure()
        {
            Post("/api/user/email/confirm/send");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var email = Query<string>("Email");
            var sendEmailConfirmationCommand = new SendEmailConfirmationCommand(email ?? "");
            await sender.Send(sendEmailConfirmationCommand, cancellationToken);
            await SendNoContentAsync(cancellationToken);
        }
    }