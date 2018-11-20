using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class SortBacklogItemsRequest : BaseRequest
    {
        public IEnumerable<TaskVM> Items { get; internal set; }
    }
}