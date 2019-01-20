using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class SprintResponse : BaseResponse
    {
        public List<TaskVM> Tasks { get; set; }
        public DateTime StartDate { get; internal set; }
        public DateTime? EndDate { get; internal set; }
        public int SprintId { get; internal set; }
    }
}