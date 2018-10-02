using System;
using System.Collections.Generic;
using System.Text;
using SecureWebAPI.Models;

namespace BusinessLogic.Response
{
    public class TodoResponse : BaseResponse
    {
        public TodoVM Todo { get; set; }
    }
}
