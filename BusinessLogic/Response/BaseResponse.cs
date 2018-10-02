using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public Dictionary<Errors,string> Errors { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}
