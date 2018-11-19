using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.Models
{
    public class TaskVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public bool BacklogItem { get; set; }
    }
}