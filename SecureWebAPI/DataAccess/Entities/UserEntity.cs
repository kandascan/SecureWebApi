using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SecureWebAPI.DataAccess.Entities
{
    public class UserEntity: IdentityUser
    { 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public virtual ICollection<TodoEntity> Todos { get; set; }
    }
}