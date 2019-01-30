using System;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class BaseRequest
    {
        public Guid RequestId { get; set; }

        public BaseRequest()
        {
            RequestId = new Guid();
        }

        public string UserId { get; set; }
        public int TeamId { get; set; }
    }
}