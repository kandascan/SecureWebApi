using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public int? TeamId { get; set; }
        public string Description { get; set; }
        public int? EffortId { get; set; }
        public int? PriorityId { get; set; }
        public string Username { get; set; }
        public int OrderId { get; set; }
        public int? Sprint { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}