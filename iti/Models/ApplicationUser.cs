using Microsoft.AspNetCore.Identity;

namespace iti.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int UserId { get; set; } 
    }
}
