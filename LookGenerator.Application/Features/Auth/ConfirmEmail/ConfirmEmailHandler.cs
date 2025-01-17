using Application.Abstractions;

namespace Application.Features.Auth.ConfirmEmail ;

    public class ConfirmEmailHandler(IUserService userService):ICommandHandler<ConfirmEmailCommand>
    {
        public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var normalizedToken = request.ConfirmationToken.Replace(' ','+').Trim();
            await userService.ConfirmEmailAsync(request.Email, normalizedToken);
        }
    }