using System.Security.Claims;
using Application.Common.DTOs;

namespace Application.Abstractions ;

    public interface ITokenGenerator
    {
        Task<string> GenerateAccessToken(UserDto userDto);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }