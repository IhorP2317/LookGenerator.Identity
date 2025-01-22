using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions;
using Application.Common.DTOs;
using LookGenerator.Persistence.Mappers;
using LookGenerator.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LookGenerator.Persistence.Services ;

    public class TokenGenerator(UserManager<ApplicationUser> userManager, ILogger<TokenGenerator> logger, IOptions<AuthSettings> authSettings): ITokenGenerator
    {
        private const int RefreshTokenSize = 32;
        
        private readonly AuthSettings _authSettings = authSettings.Value;

        public async Task<string> GenerateAccessToken(UserDto userDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            logger.LogInformation(_authSettings.SecretKey);
            var key = Encoding.ASCII.GetBytes(_authSettings.SecretKey);
            var roles = await userManager.GetRolesAsync(userDto.ToApplicationUser());
            var claimsIdentity = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Name, userDto.UserName)
                ]);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claimsIdentity.AddClaims(roleClaims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(_authSettings.AccessTokenExpirationMinutes)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken() {
            var randomNumber = new byte[RefreshTokenSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _authSettings.SymmetricSecurityKey,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }
    }