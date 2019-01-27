using SecureWebAPI.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.Models
{
    public class SortedSprintTasksVM
    {
        public int SprintId { get; set; }
        public int TeamId { get; set; }
        public List<SprintBoardTask> SprintBoardTasks { get; set; }
    }
}
