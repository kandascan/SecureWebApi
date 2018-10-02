using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class UserEntity : IdentityUser
    { 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public virtual ICollection<TodoEntity> Todos { get; set; }
    }
}