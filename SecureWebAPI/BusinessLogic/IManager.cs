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

        void CreateTask(TaskRequest request, TaskResponse response);

        void IsUserTeamMember(BaseRequest request, BaseResponse response);

        void GetBacklogTasks(GetBacklogTasksRequest request, GetBacklogTasksResponse response);

        void SortBacklogItems(SortBacklogItemsRequest request, SortBacklogItemsResponse response);

        void RemoveTask(RemoveTaskRequest request, RemoveTaskResponse response);

        void GetPriorities(GetPrioritiesRequest request, GetPrioritiesResponse response);

        void GetEfforts(GetEffortsRequest request, GetEffortsResponse response);

        void UpdateTask(TaskRequest request, TaskResponse response);

        Task CreateTeam(TeamRequest request, TeamResponse response);

        void GetUserTeams(TeamRequest request, TeamResponse response);

        void GetTeamBacklog(BacklogRequest request, BacklogResponse response);

        void GetCurrentSprint(SprintRequest request, SprintResponse response);

        void SortSprintTasks(SprintRequest request, SprintResponse response);

        void CreateSprint(SprintRequest request, SprintResponse response);

        void GetSprintsList(SprintRequest request, SprintResponse response);

        void GetAllUsersWithoutUsersInTeam(UserRequest request, UserResponse response);

        void GetTeamUsers(TeamRequest request, TeamResponse response);

        void GetUserRoles(RoleRequest request, RoleResponse response);

        void AddUserToTeam(UserRequest request, UserResponse response);

        void DeleteUserFromTeam(UserRequest request, UserResponse response);

        void GetTeamById(TeamRequest request, TeamResponse response);
    }
}