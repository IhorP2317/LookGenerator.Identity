namespace Application.Common.DTOs ;

    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string Role { get; set; }  = string.Empty;
        public string Password { get; set; }  = string.Empty;
     
        public string? RefreshToken { get; set; }
    }