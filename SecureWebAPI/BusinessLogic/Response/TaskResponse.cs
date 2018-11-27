using SecureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class TaskResponse : BaseResponse
    {
        public TaskVM Task { get; set; }
    }
}