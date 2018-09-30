using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SecureWebAPI.Models
{
    public class User : IdentityUser
    { 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }
    }
}