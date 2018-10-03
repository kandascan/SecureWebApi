using BusinessLogic.Request;
using BusinessLogic.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public interface IServiceManager
    {
        TodoResponse GetTodoById(TodoRequest request);
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
    }
}
