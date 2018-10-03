using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Response
{
    public class RegisterUserResponse : BaseResponse
    {
        public string Token { get; internal set; }
    }
}
