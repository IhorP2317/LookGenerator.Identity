using Application.Common.DTOs;
using Application.Features;

namespace Application.Abstractions ;

    public interface IUserService
    {
        Task<string> RegisterAsync(UserDto user);
        Task<UserDto> SingleAsync(string email);
        Task<UserDto> SingleAsync(Guid id);   
        Task<UserDto?> SingleOrDefaultAsync(string email);
        Task<List<string>> GetUserRolesAsync(Guid userId);
        Task<TokenDto> LoginAsync(UserDto userDto);
        Task ConfirmEmailAsync(string email, string confirmationToken);
        Task<TokenDto> RefreshAccessTokenAsync(TokenDto tokenDto);
        Task ResetPasswordAsync(string email, string resetPasswordToken, string newPassword);
        Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task DeleteUserAsync(Guid userId);
        Task<bool> IsExistAsync(string email);
        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsUserNameUniqueAsync(string username);
        Task<bool> IsBearerTokenValidAsync(TokenDto token);
        Task<bool> IsEmailConfirmedAsync(string email);
    }