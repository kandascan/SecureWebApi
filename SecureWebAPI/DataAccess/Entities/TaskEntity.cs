using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Taskname { get; set; }
        public string Description { get; set; }
        public string Effort { get; set; }
        public string Priority { get; set; }
        public string Username { get; set; }
        public int OrderId { get; set; }
        public bool BacklogItem { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}