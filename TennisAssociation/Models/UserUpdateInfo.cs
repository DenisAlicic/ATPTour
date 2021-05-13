using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TennisAssociation.Models
{
    /// <summary>
    /// Data for updating user info.
    /// </summary>
    public class UserUpdateInfo
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
