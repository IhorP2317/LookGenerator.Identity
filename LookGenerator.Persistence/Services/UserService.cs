using System.Security.Claims;
using Application.Abstractions;
using Application.DTOs;
using Application.Exceptions;
using LookGenerator.Persistence.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LookGenerator.Persistence.Services ;

    public class UserService(UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator) : IUserService
    {
        public async Task<string> RegisterAsync(UserDto userDto)
        {
            var user = userDto.ToApplicationUser();
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, userDto.Role);
            }
            else
            {
                throw new BadRequestException("The provided user details are invalid.");
            }
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            return user == null ? [] : (await userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<TokenDto> LoginAsync(UserDto userDto)
        {
            var user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                throw new NotFoundException($"User with email {userDto.Email} not found!");
            var validPassword = await userManager.CheckPasswordAsync(user, userDto.Password);
            if (!validPassword)
                throw new UnauthorizedException($"User with {userDto.Email} unauthorized!");
            user.RefreshToken = tokenGenerator.GenerateRefreshToken();
            await userManager.UpdateAsync(user);
            return new TokenDto(await tokenGenerator.GenerateAccessToken(userDto), user.RefreshToken);
        }
        

        public async Task<TokenDto> RefreshAccessTokenAsync(TokenDto tokenDto)
        {
            var principal = tokenGenerator.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var email = principal.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
            user!.RefreshToken = tokenGenerator.GenerateRefreshToken();
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new InternalServerException("Could not create refresh token!");
            return
                new TokenDto(
                    await tokenGenerator.GenerateAccessToken(
                        user.ToUserDto(principal.FindFirst(ClaimTypes.Role)!.Value)), user.RefreshToken);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException($"User with email {email} not found!");
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException($"User with email {email} not found!");
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var userToDelete = await userManager.FindByIdAsync(userId.ToString());
            if (userToDelete == null)
                throw new NotFoundException($"User with id {userId} is not exist!");
           var result =  await userManager.DeleteAsync(userToDelete);
            if (!result.Succeeded)
            {
                throw new InternalServerException(result.Errors.First().Description);
            }
        }

        public async Task ResetPasswordAsync(string email, string resetPasswordToken, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException($"User with email {email} not found!");
            var result = await userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);
            if (!result.Succeeded)
                throw new InternalServerException(result.Errors.First().Description);
        }

        public async Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException($"User with id {userId} does not exist!");
            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new InternalServerException(result.Errors.First().Description);
            }
        }
        public async Task<UserDto> SingleAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException($"User with email {user?.Email} not found!");
            var role = (await userManager.GetRolesAsync(user)).First();
            return user.ToUserDto(role);
        }

        public async Task<UserDto?> SingleOrDefaultAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return null;
            var role = (await userManager.GetRolesAsync(user)).First();
            return user.ToUserDto(role);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser applicationUser)
        {
            return await userManager.GetRolesAsync(applicationUser);
        }

        public async Task<UserDto> SingleAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NotFoundException($"User with Id {id} not found!");
            var role = (await userManager.GetRolesAsync(user)).First();
            return user.ToUserDto(role);
        }

        public async Task ConfirmEmailAsync(string email, string confirmationToken)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException($"User with email {email} does not exist!");
            }
            var result = await userManager.ConfirmEmailAsync(user, confirmationToken);
            if (!result.Succeeded)
                throw new InternalServerException(result.Errors.First().Description);
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return (await userManager.FindByIdAsync(id.ToString())) != null;
        }
        public async Task<bool> IsExistAsync(string email)
        {
            return (await userManager.FindByEmailAsync(email)) != null;
        }
        public async Task<bool> IsUserNameUniqueAsync(string username)
        {
            return (await userManager.FindByNameAsync(username)) == null;
        }

        public async Task<bool> IsBearerTokenValidAsync(TokenDto token)
        {
            var principal = tokenGenerator.GetPrincipalFromExpiredToken(token.AccessToken);
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return false;
            var user = await userManager.FindByEmailAsync(email);
            return user != null && user.RefreshToken == token.RefreshToken;
        }

        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            return await userManager.Users.AsNoTracking().AnyAsync(u => u.Email == email && u.EmailConfirmed);
        }
    }