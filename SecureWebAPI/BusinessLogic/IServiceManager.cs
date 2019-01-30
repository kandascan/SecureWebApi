using System.Threading.Tasks;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;

namespace SecureWebAPI.BusinessLogic
{
    public interface IServiceManager
    {
        BaseResponse IsUserTeamMember(BaseRequest request);

        Task<UserResponse> RegisterUser(UserRequest request);

        Task<UserResponse> LoginUser(UserRequest request);

        Task<UserResponse> LogOutUser(UserRequest request);

        TaskResponse CreateTask(TaskRequest request);

        TaskResponse GetTaskById(TaskRequest request);

        GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request);

        BacklogResponse GetTeamBacklog(BacklogRequest request);

        SortBacklogItemsResponse SortBacklogItems(SortBacklogItemsRequest request);

        RemoveTaskResponse RemoveTask(RemoveTaskRequest request);

        GetPrioritiesResponse GetPriorities(GetPrioritiesRequest request);

        GetEffortsResponse GetEfforts(GetEffortsRequest request);

        TaskResponse UpdateTask(TaskRequest request);

        TeamResponse CreateTeam(TeamRequest request);

        TeamResponse GetUserTeams(TeamRequest request);

        SprintResponse GetCurrentSprint(SprintRequest request);

        SprintResponse CreateSprint(SprintRequest request);

        UserResponse DeleteUserFromTeam(UserRequest request);

        SprintResponse GetSprintsList(SprintRequest request);

        UserResponse GetAllUsersWithouUsersInTeam(UserRequest request);

        TeamResponse GetTeamUsers(TeamRequest request);

        RoleResponse GetUserRoles(RoleRequest request);

        UserResponse AddUserToTeam(UserRequest request);

        SprintResponse SortSprintTasks(SprintRequest request);

        TeamResponse GetTeamById(TeamRequest request);
    }
}