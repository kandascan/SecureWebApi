using System.Threading.Tasks;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;

namespace SecureWebAPI.BusinessLogic
{
    public interface IServiceManager
    {
        Task<UserResponse> RegisterUser(UserRequest request);
        Task<UserResponse> LoginUser(UserRequest request);
        Task<UserResponse> LogOutUser(UserRequest request);
        TodoResponse GetTodoById(TodoRequest request);
    }
}