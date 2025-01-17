using System.Security.Claims;
using Application.DTOs;

namespace Application.Abstractions ;

    public interface ITokenGenerator
    {
        Task<string> GenerateAccessToken(UserDto userDto);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }