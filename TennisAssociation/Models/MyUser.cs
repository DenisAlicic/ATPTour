using Microsoft.AspNetCore.Identity;

namespace TennisAssociation.Models
{
    public class MyUser : IdentityUser
    {
        public bool IsAdmin { get; set; } 
    }
}