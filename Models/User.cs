using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Auth.Models
{
    public class User : IdentityUser
    {
        public DateTime LastAccessed { get; set; }
        public virtual DateTime? RegistrationDate { get; set; }
        public bool IsLocked { get; set; }



    }

}   
