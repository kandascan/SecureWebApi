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

        TodoResponse CreateTodo(TodoRequest request);

        TodoResponse GetTodoById(TodoRequest request);

        TodoResponse GetUserTodos(TodoRequest request);

        TodoResponse GetAllTodos(TodoRequest request);

        TodoResponse UpdateTodo(TodoRequest request);

        TodoResponse RemoveTodo(TodoRequest request);

        TaskResponse CreateTask(TaskRequest request);

        TaskResponse GetAllTasks(TaskRequest request);

        GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request);

        SortBacklogItemsResponse SortBacklogItems(SortBacklogItemsRequest request);

        RemoveTaskResponse RemoveTask(RemoveTaskRequest request);
    }
}