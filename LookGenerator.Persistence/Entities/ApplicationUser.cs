using Microsoft.AspNetCore.Identity;

namespace LookGenerator.Persistence ;

    public class ApplicationUser:IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }

    }