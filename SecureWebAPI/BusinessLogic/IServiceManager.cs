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
              
        TaskResponse CreateTask(TaskRequest request);

        TaskResponse GetAllTasks(TaskRequest request);

        GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request);

        SortBacklogItemsResponse SortBacklogItems(SortBacklogItemsRequest request);

        RemoveTaskResponse RemoveTask(RemoveTaskRequest request);
    }
}