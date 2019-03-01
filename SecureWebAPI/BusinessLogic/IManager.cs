using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic
{
    public interface IManager
    {
        void GetTaskById(TaskRequest request, TaskResponse response);

        Task RegisterUser(UserRequest request, UserResponse response);

        Task LoginUser(UserRequest request, UserResponse response);

        Task LogOutUser(UserRequest request, UserResponse response);
    }
}