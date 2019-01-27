using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.Models
{
    public class SprintTaskVM
    {       
        public int? TaskId { get; set; }
        public string TaskName { get; set; }
        public string UserName { get; set; }
        public int Effort { get; set; }
    }
}
