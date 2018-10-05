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
        public string ResponseTime { get; set; } = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
        public string Message { get; set; }
    }
}