using Application.Common.DTOs;
using Riok.Mapperly.Abstractions;

namespace Application.Features.Auth ;
    [Mapper]
    public partial class RegisterMapper
    {
        [MapperIgnoreTarget(nameof(UserDto.Id))]
        [MapperIgnoreTarget(nameof(UserDto.EmailConfirmed))]
        public partial UserDto ToUserDto(RegisterCommand registerCommand);
        
        [MapperIgnoreSource(nameof(UserDto.Password))]
        public partial RegisterResponse ToRegisterResponse(UserDto userDto);
    }
