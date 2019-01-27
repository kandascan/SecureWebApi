using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.BusinessLogic.Model;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class SprintResponse : BaseResponse
    {
        public SprintResponse()
        {
            SprintBoardTasks = new List<SprintBoardTask>();
        }
        public List<TaskVM> Tasks { get; set; }
        public DateTime StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }
        public int SprintId { get;  set; }
        public int TeamId { get;  set; }
        public List<ShortSprint> SprintsList { get;  set; }
        public string SprintName { get;  set; }
        public List<SprintBoardTask> SprintBoardTasks { get; set; }
    }
}