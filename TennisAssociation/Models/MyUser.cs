using Microsoft.AspNetCore.Identity;

namespace TennisAssociation.Models
{
    /// <summary>
    /// Main class for handling users of system.
    /// </summary>
    public class MyUser : IdentityUser
    {
        public bool IsAdmin { get; set; } 
    }
}