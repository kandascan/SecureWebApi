using System;
using System.Collections.Generic;
using SecureWebAPI.Enums;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new Dictionary<Errors, string>();
        }
        public bool Success { get; set; }
        public Dictionary<Errors,string> Errors { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}