using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TennisAssociation.Models
{
    public partial class Fan
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender {get; set; }
        public DateTime? Birthday { get; set; }
        public string Email {get; set;}
        [Key, Column(Order = 0)]
        public string Username {get; set; }
        public string Password {get; set; }
    }
}