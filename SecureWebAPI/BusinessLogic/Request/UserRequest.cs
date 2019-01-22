using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class UserRequest : BaseRequest
    {
        public UserVM User { get; set; }
        public string UserId { get; internal set; }
    }
}