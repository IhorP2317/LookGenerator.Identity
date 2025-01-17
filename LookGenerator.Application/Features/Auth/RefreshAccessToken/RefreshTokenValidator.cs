using Application.Abstractions;
using FluentValidation;

namespace Application.Features.Auth.RefreshAccessToken ;

    public class RefreshTokenValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator(IUserService userService)
        {
           
            RuleFor(r => r.Token.AccessToken).NotEmpty().WithMessage("Access token is required.");

            
            RuleFor(r => r.Token.RefreshToken).NotEmpty().WithMessage("Refresh token is required.");
            
            RuleFor(r => r.Token)
                .MustAsync(async (token, _) => await userService.IsBearerTokenValidAsync(token))
                .WithMessage("Invalid access or refresh token.");
        }
    }