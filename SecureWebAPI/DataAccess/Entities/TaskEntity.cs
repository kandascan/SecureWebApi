using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }

        //public virtual UserEntity User { get; set; }
        public int OrderId { get; set; }

        public bool BacklogItem { get; set; }
    }
}