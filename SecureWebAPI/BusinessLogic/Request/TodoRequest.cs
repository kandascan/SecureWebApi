using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class TodoRequest : BaseRequest
    {
        public int TodoId { get; set; }
        public TodoVM Todo { get; set; }
        public string UserId { get; set; }
    }
}