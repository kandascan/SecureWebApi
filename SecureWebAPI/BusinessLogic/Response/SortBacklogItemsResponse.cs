using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class SortBacklogItemsResponse : BaseResponse
    {
        public List<TaskVM> Tasks { get; internal set; }
    }
}