using BusinessLogic.Request;
using BusinessLogic.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Service
{
    public interface IServiceManager
    {
        TodoResponse GetTodoById(TodoRequest request);
    }
}
