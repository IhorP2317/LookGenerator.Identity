using System.Security.Claims;
using Application.Abstractions;

namespace LookGenerator.WebAPI.Services ;

    public class CurrentUserService(IHttpContextAccessor httpContextAccessor):ICurrentUserService
    {
        public string? UserId =>
            httpContextAccessor.HttpContext?.User.FindFirstValue("nameid");

        public string? Username =>
            httpContextAccessor.HttpContext?.User.FindFirstValue("unique_name");

        public string? Email =>
            httpContextAccessor.HttpContext?.User.FindFirstValue("email");

        public string? UserRole =>
            httpContextAccessor.HttpContext?.User.FindFirstValue("role");
    }