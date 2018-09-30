using System;

namespace SecureWebAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}