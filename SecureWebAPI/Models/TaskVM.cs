using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.Models
{
    public class TaskVM
    {
        public int Id { get; set; }
        public string Taskname { get; set; }
        public int? TeamId { get; set; }
        public string Description { get; set; }
        public int? EffortId { get; set; }
        public int? PriorityId { get; set; }
        public string Username { get; set; }
        public int OrderId { get; set; }
        public int? Sprint { get; set; }
    }
}