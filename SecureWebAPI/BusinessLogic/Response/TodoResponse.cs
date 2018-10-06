using System.Collections.Generic;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class TodoResponse : BaseResponse
    {
        public TodoVM Todo { get; set; }
        public List<TodoVM> Todos { get; set; }
    }
}