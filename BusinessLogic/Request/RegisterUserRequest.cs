using BusinessLogic.Models;

namespace BusinessLogic.Request
{
    public class RegisterUserRequest : BaseRequest
    {
        public User User { get; set; }
    }
}
