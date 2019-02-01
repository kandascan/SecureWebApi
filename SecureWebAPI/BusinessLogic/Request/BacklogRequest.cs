using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class BacklogRequest : BaseRequest
    {
        public int BacklogId { get; set; }
    }
}