using Application.Features.User_Management.DeleteUser;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LookGenerator.WebAPI.Endpoints.User_Management ;

    public class DeleteUserEndpoint(ISender sender):EndpointWithoutRequest
    {
        public override void Configure()
        {
            Delete("/api/user/{userId}");
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
            Policies("CanDeleteUser");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var userIdToDelete = Route<Guid>("userId");
            await sender.Send(new DeleteUserCommand(userIdToDelete), cancellationToken);
            await SendNoContentAsync(cancellationToken);
        }
    }