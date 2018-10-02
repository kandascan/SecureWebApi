using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Request
{
    public class TodoRequest : BaseRequest
    {
        public int TodoId { get; internal set; }
    }
}
