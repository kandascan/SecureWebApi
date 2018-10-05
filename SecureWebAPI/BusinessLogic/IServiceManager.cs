using System.Threading.Tasks;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;

namespace SecureWebAPI.BusinessLogic
{
    public interface IServiceManager
    {
        TodoResponse GetTodoById(TodoRequest request);
        Task<UserResponse> RegisterUser(UserRequest request);
        Task<UserResponse> LoginUser(UserRequest request);

    }
}