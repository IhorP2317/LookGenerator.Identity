using System.Security.Claims;
using Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Security.DeleteUser ;

    public class DeleteUserHandler(IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor)
        : AuthorizationHandler<DeleteUserRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DeleteUserRequirement requirement)
        {
            using var scope = serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                context.Fail();
                return;
            }

            var routeValues = httpContext.Request.RouteValues;
            if (!routeValues.TryGetValue("userId", out var userIdObj) || userIdObj == null)
            {
                context.Fail();
                return;
            }

            if (!Guid.TryParse(userIdObj.ToString(), out var userIdToDelete))
            {
                context.Fail();
                return;
            }

            var rolesToDelete = await userService.GetUserRolesAsync(userIdToDelete);

            if (rolesToDelete.Count < 1)
            {
                context.Fail();
                return;
            }

            if (rolesToDelete.Contains("Admin"))
            {
                context.Fail();
                return;
            }


            var currentUserRole = currentUserService.UserRole;

            if (rolesToDelete.Contains("User") && currentUserRole != "Admin" &&
                currentUserService.UserId != userIdToDelete.ToString())
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }