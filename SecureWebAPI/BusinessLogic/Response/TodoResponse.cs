using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class TodoResponse : BaseResponse
    {
        public TodoVM Todo { get; set; }
    }
}