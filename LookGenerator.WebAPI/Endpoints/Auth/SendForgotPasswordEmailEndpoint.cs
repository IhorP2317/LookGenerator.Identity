using Application.Features.Auth.SendForgotPasswordEmail;
using FastEndpoints;
using MediatR;

namespace LookGenerator.WebAPI.Endpoints.Auth ;

    public class SendForgotPasswordEmailEndpoint(ISender sender) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Post("/api/user/password/forget");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var email = Query<string>("Email");
            var sendForgotPasswordCommand = new SendForgotPasswordEmailCommand(email ?? "");
            await sender.Send(sendForgotPasswordCommand, cancellationToken);
            await SendNoContentAsync(cancellationToken);
        }
    }