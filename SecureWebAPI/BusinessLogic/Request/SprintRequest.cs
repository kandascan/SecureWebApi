using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class SprintRequest : BaseRequest
    {
        public int Id { get; set; }
        public int SprintId { get; set; }
        public int TaskId { get; set; }
    }
}