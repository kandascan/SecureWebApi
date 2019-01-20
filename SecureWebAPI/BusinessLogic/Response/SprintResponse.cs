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
        public List<TaskVM> Tasks { get; set; }
        public DateTime StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }
        public int SprintId { get;  set; }
        public int TeamId { get;  set; }
        public List<ShortSprint> SprintsList { get; internal set; }
        public string SprintName { get; internal set; }
    }
}