using Application.Common.DTOs;

namespace LookGenerator.Persistence.Mappers ;

    public static class ApplicationUserMapper
    {
        public static ApplicationUser ToApplicationUser(this UserDto user)
        {
            return new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                Id = user.Id,
                NormalizedUserName = user.UserName.ToUpper(), 
                NormalizedEmail = user.Email.ToUpper(),     
                EmailConfirmed = user.EmailConfirmed,
                RefreshToken = user.RefreshToken                                     
            };
        }
        public static UserDto ToUserDto(this ApplicationUser user, string role)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                EmailConfirmed = user.EmailConfirmed,
                Role = role,
                RefreshToken = user.RefreshToken
            };
        }
    }