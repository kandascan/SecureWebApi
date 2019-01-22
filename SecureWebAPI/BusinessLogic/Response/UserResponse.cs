using System.Collections.Generic;
using SecureWebAPI.BusinessLogic.Model;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class UserResponse : BaseResponse
    {
        public string Token { get; set; }
        public List<User> UserList { get; internal set; }
    }
}