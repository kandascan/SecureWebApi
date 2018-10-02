using System;

namespace DataAccess.Entities
{
    public class TodoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}