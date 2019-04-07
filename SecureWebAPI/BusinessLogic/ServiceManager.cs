using System.Reflection;
using System.Threading.Tasks;
using LoggerService;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using SecureWebAPI.DataAccess.UnitOfWork;

namespace SecureWebAPI.BusinessLogic
{
    public class ServiceManager : BaseService, IServiceManager
    {
        private readonly IManager _manager;

        public ServiceManager(IManager manager, ILoggerManager logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
            _manager = manager;
        }

        public async Task<UserResponse> RegisterUser(UserRequest request)
        {
            var response = new UserResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                await _manager.RegisterUser(request, response);
            });

            return response;
        }

        public async Task<UserResponse> LoginUser(UserRequest request)
        {
            var response = new UserResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                await _manager.LoginUser(request, response);
            });

            return response;
        }

        public async Task<UserResponse> LogOutUser(UserRequest request)
        {
            var response = new UserResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                await _manager.LogOutUser(request, response);
            });

            return response;
        }

        public TaskResponse CreateTask(TaskRequest request)
        {
            var response = new TaskResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.CreateTask(request, response);
            });

            return response;
        }

        public TaskResponse GetTaskById(TaskRequest request)
        {
            var response = new TaskResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetTaskById(request, response);
            });
            return response;
        }

        public BaseResponse IsUserTeamMember(BaseRequest request)
        {
            var response = new BaseResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.IsUserTeamMember(request, response);
            });
            return response;
        }

        public GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request)
        {
            var response = new GetBacklogTasksResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetBacklogTasks(request, response);
            });
            return response;
        }

        public SortBacklogItemsResponse SortBacklogItems(SortBacklogItemsRequest request)
        {
            var response = new SortBacklogItemsResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.SortBacklogItems(request, response);
            });
            return response;
        }

        public GetPrioritiesResponse GetPriorities(GetPrioritiesRequest request)
        {
            var response = new GetPrioritiesResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetPriorities(request, response);
            });

            return response;
        }

        public GetEffortsResponse GetEfforts(GetEffortsRequest request)
        {
            var response = new GetEffortsResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetEfforts(request, response);
            });

            return response;
        }

        public TaskResponse UpdateTask(TaskRequest request)
        {
            var response = new TaskResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.UpdateTask(request, response);
            });

            return response;
        }

        public async Task<TeamResponse> CreateTeam(TeamRequest request)
        {
            var response = new TeamResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                await _manager.CreateTeam(request, response);
            });

            return response;
        }

        public TeamResponse GetUserTeams(TeamRequest request)
        {
            var response = new TeamResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetUserTeams(request, response);
            });
            return response;
        }

        public BacklogResponse GetTeamBacklog(BacklogRequest request)
        {
            var response = new BacklogResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetTeamBacklog(request, response);
            });
            return response;
        }

        public SprintResponse SortSprintTasks(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.SortSprintTasks(request, response);
            });
            return response;
        }

        public SprintResponse CreateSprint(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.CreateSprint(request, response);
            });
            return response;
        }

        public SprintResponse GetSprintsList(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetSprintsList(request, response);
            });
            return response;
        }

        public UserResponse GetAllUsersWithoutUsersInTeam(UserRequest request)
        {
            var response = new UserResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetAllUsersWithoutUsersInTeam(request, response);
            });
            return response;
        }

        public TeamResponse GetTeamUsers(TeamRequest request)
        {
            var response = new TeamResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetTeamUsers(request, response);
            });
            return response;
        }

        public RoleResponse GetUserRoles(RoleRequest request)
        {
            var response = new RoleResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetUserRoles(request, response);
            });
            return response;
        }

        public UserResponse AddUserToTeam(UserRequest request)
        {
            var response = new UserResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.AddUserToTeam(request, response);
            });
            return response;
        }

        public UserResponse DeleteUserFromTeam(UserRequest request)
        {
            var response = new UserResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.DeleteUserFromTeam(request, response);
            });
            return response;
        }

        public TeamResponse GetTeamById(TeamRequest request)
        {
            var response = new TeamResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                _manager.GetTeamById(request, response);
            });
            return response;
        }

        public async Task<RemoveTaskResponse> RemoveTask(RemoveTaskRequest request)
        {
            var response = new RemoveTaskResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                await _manager.RemoveTask(request, response);
            });

            return response;
        }

        public async Task<SprintResponse> GetCurrentSprint(SprintRequest request)
        {
            var response = new SprintResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                await _manager.GetCurrentSprint(request, response);
            });

            return response;
        }
    }
}