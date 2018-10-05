using System;
using System.Collections.Generic;
using SecureWebAPI.Enums;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new Dictionary<string, string>();
        }
        public bool Success { get; set; }
        public Dictionary<string, string> Errors { get; set; }

        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}