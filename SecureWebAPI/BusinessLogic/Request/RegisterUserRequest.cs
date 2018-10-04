using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class RegisterUserRequest : BaseRequest
    {
        public UserVM User { get; set; }
    }
}