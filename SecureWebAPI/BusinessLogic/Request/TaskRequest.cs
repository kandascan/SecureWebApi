using SecureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class TaskRequest : BaseRequest
    {
        public int TaskId { get; set; }
        public TaskVM Task { get; set; }
        public string UserId { get; set; }
    }
}