using System.Threading.Tasks;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;

namespace SecureWebAPI.BusinessLogic
{
    public interface IServiceManager
    {
        TodoResponse GetTodoById(TodoRequest request);
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
    }
}