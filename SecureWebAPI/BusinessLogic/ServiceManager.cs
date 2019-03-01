using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using SecureWebAPI.BusinessLogic.Model;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.DataAccess.UnitOfWork;
using SecureWebAPI.Enums;
using SecureWebAPI.Extensions;
using SecureWebAPI.Helpers;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic
{
    public class ServiceManager : BaseService, IServiceManager
    {
        private readonly IMapper _mapper; // dell
        private readonly IConfiguration _configuration; // dell
        private readonly IManager _manager;

        public ServiceManager(IUnitOfWork uow, IMapper mapper, IManager manager, IConfiguration configuration, ILoggerManager logger) : base(logger)
        {
            _mapper = mapper; // dell
            _configuration = configuration; // dell
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
                response.Errors = Validator.CreateTask(request.Task);
                if (response.Errors.Count() > 0)
                {
                    return;
                }
                var newTask = _mapper.Map<TaskEntity>(request.Task);
                var task = uow.Repository<TaskEntity>().Add(newTask);
                uow.Save();
                if (task != null)
                {
                    if (task.Sprint != -1)
                    {
                        var xRefSprintTask = new XRefSprintTaskEntity { SprintId = (int)task.Sprint, TaskId = task.Id };
                        uow.Repository<XRefSprintTaskEntity>().Add(xRefSprintTask);
                        var backlogTaskDb = uow.Repository<XRefBacklogTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                        if (backlogTaskDb != null)
                        {
                            uow.Repository<XRefBacklogTaskEntity>().Delete(backlogTaskDb);
                        }
                    }
                    else
                    {
                        var backlog = uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.Task.TeamId).FirstOrDefault();
                        var xRefBacklogTask = new XRefBacklogTaskEntity { TaskId = task.Id, BacklogId = backlog.BacklogId };
                        uow.Repository<XRefBacklogTaskEntity>().Add(xRefBacklogTask);
                        var sprintTaskDb = uow.Repository<XRefSprintTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                        if (sprintTaskDb != null)
                        {
                            uow.Repository<XRefSprintTaskEntity>().Delete(sprintTaskDb);
                        }
                    }
                    uow.Save();
                    response.Task = _mapper.Map<TaskVM>(task);
                }
                else
                {
                    response.Errors.Add("Create error", "Cannot create task");
                }
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
                var teamMember = uow.Repository<XRefTeamUserEntity>().GetOverview(u => u.UserId == request.UserId && u.TeamId == request.TeamId).FirstOrDefault();
                if (teamMember != null)
                    response.IsTeamMember = true;
            });
            return response;
        }

        public GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request)
        {
            var response = new GetBacklogTasksResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var backlog = uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.TeamId).FirstOrDefault();

                var xRefBacklogTask = uow.Repository<XRefBacklogTaskEntity>().GetOverview(x => x.BacklogId == backlog.BacklogId).Select(t => t.TaskId);

                var tasks = uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
                if (tasks != null)
                {
                    response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
                }
                else
                {
                    response.Errors.Add("Get Tasks", "Cannot featch Tasks");
                }
            });
            return response;
        }

        public SortBacklogItemsResponse SortBacklogItems(SortBacklogItemsRequest request)
        {
            var response = new SortBacklogItemsResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var ids = request.Items.Select(i => i.Id).ToArray();
                var dbTasks = uow.Repository<TaskEntity>().GetOverview(i => ids.Contains(i.Id)).OrderBy(o => ids.IndexOf(o.Id)).ToList();

                if (dbTasks != null && dbTasks.Count > 0)
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        dbTasks[i].OrderId = i;
                    }
                    uow.Save();
                    response.Tasks = _mapper.Map<List<TaskVM>>(dbTasks);
                }
                else
                {
                    response.Errors.Add("Sort Backlog items", "Cannot featch Backlog items");
                }
            });
            return response;
        }

        public RemoveTaskResponse RemoveTask(RemoveTaskRequest request)
        {
            var response = new RemoveTaskResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var dbTask = uow.Repository<TaskEntity>().GetDetails(t => t.Id == request.Id);
                if (dbTask != null)
                {
                    uow.Repository<TaskEntity>().Delete(dbTask);

                    uow.Save();
                    var backlogItems = GetBacklogTasks(new GetBacklogTasksRequest { TeamId = dbTask.TeamId });
                    if (backlogItems != null && backlogItems.Tasks.Count > 0)
                    {
                        response.Tasks = backlogItems.Tasks;
                    }
                    else
                    {
                        response.Tasks = new List<TaskVM>();
                    }
                }
                else
                {
                    response.Errors.Add("Sort Backlog items", "Cannot featch Backlog items");
                }
            });

            return response;
        }

        public GetPrioritiesResponse GetPriorities(GetPrioritiesRequest request)
        {
            var response = new GetPrioritiesResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var dbPriorites = uow.Repository<PriorityEntity>().GetAll();
                if (dbPriorites != null && dbPriorites.Count() > 0)
                {
                    response.Priorities = _mapper.Map<List<PriorityVM>>(dbPriorites);
                    response.Success = true;
                }
            });

            return response;
        }

        public GetEffortsResponse GetEfforts(GetEffortsRequest request)
        {
            var response = new GetEffortsResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var dbEfforts = uow.Repository<EffortEntity>().GetAll();
                if (dbEfforts != null && dbEfforts.Count() > 0)
                {
                    response.Efforts = _mapper.Map<List<EffortVM>>(dbEfforts);
                    response.Success = true;
                }
            });

            return response;
        }

        public TaskResponse UpdateTask(TaskRequest request)
        {
            var response = new TaskResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var editedTask = _mapper.Map<TaskEntity>(request.Task);
                var task = uow.Repository<TaskEntity>().Update(editedTask);
                if (task != null)
                {
                    if (task.Sprint != -1)
                    {
                        var xRefSprintTask = new XRefSprintTaskEntity { SprintId = (int)task.Sprint, TaskId = task.Id, OrderId = -1 };
                        var xst = uow.Repository<XRefSprintTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                        if (xst == null)
                            uow.Repository<XRefSprintTaskEntity>().Add(xRefSprintTask);
                        var backlogTaskDb = uow.Repository<XRefBacklogTaskEntity>().GetOverview(t => t.TaskId == editedTask.Id).FirstOrDefault();
                        if (backlogTaskDb != null)
                        {
                            uow.Repository<XRefBacklogTaskEntity>().Delete(backlogTaskDb);
                        }
                    }
                    else
                    {
                        var backlog = uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.Task.TeamId).FirstOrDefault();
                        var xRefBacklogTask = new XRefBacklogTaskEntity { TaskId = task.Id, BacklogId = backlog.BacklogId };
                        var xbt = uow.Repository<XRefBacklogTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                        if (xbt == null)
                            uow.Repository<XRefBacklogTaskEntity>().Add(xRefBacklogTask);
                        var sprintTaskDb = uow.Repository<XRefSprintTaskEntity>().GetOverview(t => t.TaskId == editedTask.Id).FirstOrDefault();
                        if (sprintTaskDb != null)
                        {
                            uow.Repository<XRefSprintTaskEntity>().Delete(sprintTaskDb);
                        }
                    }
                    uow.Save();
                    response.Task = _mapper.Map<TaskVM>(task);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Update error", "Cannot update task");
                }
            });

            return response;
        }

        public async Task<TeamResponse> CreateTeam(TeamRequest request)
        {
            var response = new TeamResponse();
            await RunCodeAsync(MethodBase.GetCurrentMethod().Name, request, response, async (uow) =>
            {
                response.Errors = Validator.CreateTeam(request.Team);
                if (response.Errors.Count() > 0)
                {
                    return;
                }
                var newTeam = _mapper.Map<TeamEntity>(request.Team);
                uow.Repository<TeamEntity>().Add(newTeam);
                uow.Save();

                var newXrefTeamUser = new XRefTeamUserEntity { TeamId = newTeam.TeamId, UserId = request.UserId, RoleId = (int)Enums.Role.SCRUM_MASTER };
                uow.Repository<XRefTeamUserEntity>().Add(newXrefTeamUser);
                uow.Save();

                var backlog = new BacklogEntity { TeamId = newTeam.TeamId };
                uow.Repository<BacklogEntity>().Add(backlog);
                uow.Save();
                var user = uow.Repository<UserEntity>().GetOverview(u => u.Id == request.UserId).FirstOrDefault();
                var userTeams = uow.Repository<XRefTeamUserEntity>().GetOverview(u => u.UserId == user.Id).Select(t => t.TeamId).ToArray();
                response.Token = await user.GenerateJwtToken(_configuration, userTeams);

                response.Team = _mapper.Map<TeamVM>(newTeam);
                response.Success = true;
            });

            return response;
        }

        public TeamResponse GetUserTeams(TeamRequest request)
        {
            var response = new TeamResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var teams = uow.Repository<TeamEntity>().GetOverview();
                var xrefTeamsUsers = uow.Repository<XRefTeamUserEntity>().GetOverview();
                var userRoles = uow.Repository<RoleEntity>().GetOverview();
                var backlog = uow.Repository<BacklogEntity>().GetOverview();
                var xrefBacklogTask = uow.Repository<XRefBacklogTaskEntity>().GetOverview();
                var userTeams = (from ut in xrefTeamsUsers
                                 join t in teams
                                 on ut.TeamId equals t.TeamId
                                 join r in userRoles
                                 on ut.RoleId equals r.RoleId
                                 where ut.UserId == request.UserId
                                 select new UserTeams
                                 {
                                     TeamId = t.TeamId,
                                     TeamName = t.TeamName,
                                     UserRole = r.RoleName,
                                     ScrumMasterUser = r.RoleId == (int)Enums.Role.SCRUM_MASTER ? true : false,
                                     TeamUserNumber = xrefTeamsUsers.Where(x => x.TeamId == t.TeamId).Count(),
                                     TaskNumber = (from xbt in xrefBacklogTask
                                                   join b in backlog
                                                   on xbt.BacklogId equals b.BacklogId
                                                   where b.TeamId == t.TeamId
                                                   select xbt.TaskId).Count()
                                 }).OrderByDescending(t => t.TeamId).ToList();

                if (userTeams != null && userTeams.Count() > 0)
                {
                    response.UserTeams = userTeams;
                }
                else
                {
                    response.UserTeams = new List<UserTeams>();
                }
            });
            return response;
        }

        public BacklogResponse GetTeamBacklog(BacklogRequest request)
        {
            var response = new BacklogResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var teamBacklog = uow.Repository<BacklogEntity>().GetOverview().Where(b => b.TeamId == request.TeamId).FirstOrDefault();
                if (teamBacklog != null)
                {
                    var xRefBacklogTask = uow.Repository<XRefBacklogTaskEntity>().GetOverview().Where(x => x.BacklogId == teamBacklog.BacklogId).Select(i => i.TaskId);
                    var tasks = uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
                    if (tasks != null)
                    {
                        response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
                    }
                }
                else
                {
                    response.Errors.Add("Get Tasks", "Cannot featch Tasks");
                }
            });
            return response;
        }

        public SprintResponse GetCurrentSprint(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var sprint = uow.Repository<SprintEntity>().GetOverview().Where(b => b.TeamId == request.TeamId).OrderByDescending(s => s.SprintId).FirstOrDefault();
                var sprintColumn = uow.Repository<SprintColumnEntity>().GetOverview();
                var xRefSprintTask = uow.Repository<XRefSprintTaskEntity>().GetOverview();
                var task = uow.Repository<TaskEntity>().GetOverview();
                var user = uow.Repository<UserEntity>().GetOverview();
                var effort = uow.Repository<EffortEntity>().GetOverview();
                if (sprint != null)
                {
                    response.SprintName = sprint.SprintName;
                    response.EndDate = sprint.EndDate;
                    response.StartDate = sprint.StartDate;

                    var tasks =
                       (from xst in xRefSprintTask
                        join sc in sprintColumn on xst.ColumnId equals sc.ColumnId
                        join t in task on xst.TaskId equals t.Id
                        join u in user on t.UserId equals u.Id into uX
                        from u in uX.DefaultIfEmpty()
                        join e in effort on t.EffortId equals e.EffortId
                        where xst.SprintId == sprint.SprintId
                        select new SprintTask
                        {
                            Id = xst.Id,
                            ColumnId = sc.ColumnId,
                            ColumnName = sc.ColumnName,
                            OrderId = xst == null ? 0 : xst.OrderId,
                            SprintId = xst == null ? 0 : xst.SprintId,
                            TaskId = t == null ? 0 : t.Id,
                            TaskName = t?.TaskName,
                            UserName = u?.UserName,
                            Effort = e.EffortName,
                            CreatedDate = xst.CreatedDate
                        }).ToList();

                    foreach (var s in sprintColumn)
                    {
                        var items = tasks.Where(t => t.ColumnId == s.ColumnId).OrderBy(i => i.OrderId).ThenBy(d => d.CreatedDate).ToList();

                        var sprintBoardTask = new SprintBoardTask
                        {
                            ColumnName = s.ColumnName,
                            ColumnId = s.ColumnId,
                            Items = _mapper.Map<List<SprintTaskVM>>(items)
                        };

                        response.SprintBoardTasks.Add(sprintBoardTask);
                    }

                    response.TeamId = request.TeamId;
                    response.SprintId = sprint.SprintId;
                }
                else
                {
                    response.Message = $"There is no active sprint yet for team: {request.TeamId}";
                }
            });
            return response;
        }

        public SprintResponse SortSprintTasks(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var sprintColumn = uow.Repository<SprintColumnEntity>().GetOverview();
                var xRefSprintTask = uow.Repository<XRefSprintTaskEntity>().GetOverview();
                int orderId = 0;
                foreach (var col in request.SortedSprintTasks.SprintBoardTasks)
                {
                    foreach (var t in col.Items)
                    {
                        var xst = new XRefSprintTaskEntity
                        {
                            Id = t.Id,
                            SprintId = request.SortedSprintTasks.SprintId,
                            TaskId = (int)t.TaskId,
                            ColumnId = col.ColumnId,
                            OrderId = orderId
                        };
                        uow.Repository<XRefSprintTaskEntity>().Update(xst);
                        orderId++;
                    }
                }
                uow.Save();
            });
            return response;
        }

        public SprintResponse CreateSprint(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var oldSprint = request.Sprint.SprintId;
                int sprintNumber = 1;
                string sprintName = string.Empty;
                var teamName = uow.Repository<TeamEntity>().GetOverview()
                       .Where(t => t.TeamId == request.Sprint.TeamId)
                       .Select(n => n.TeamName).FirstOrDefault();
                if (!string.IsNullOrEmpty(teamName))
                {
                    sprintName = $"{teamName} - Sprint: {sprintNumber}";
                }

                var teamSprints = uow.Repository<SprintEntity>().GetOverview()
                    .Where(b => b.TeamId == request.Sprint.TeamId)
                    .Select(s => s.SprintId).ToList();
                if (teamSprints != null && teamSprints.Count > 0)
                {
                    sprintNumber = teamSprints.Count;
                    sprintName = $"{teamName} - Sprint: {++sprintNumber}";
                }

                var newSprint = new SprintEntity
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(14),
                    SprintName = sprintName,
                    TeamId = request.Sprint.TeamId
                };
                uow.Repository<SprintEntity>().Add(newSprint);
                uow.Save();

                if (oldSprint != 0)
                {
                    var notCompletedTasks = uow.Repository<XRefSprintTaskEntity>().GetOverview(s => s.SprintId == oldSprint && s.ColumnId != (int)Column.DONE).ToList();
                    notCompletedTasks.ForEach(t =>
                    {
                        uow.Repository<XRefSprintTaskEntity>().Add(new XRefSprintTaskEntity
                        {
                            ColumnId = t.ColumnId,
                            OrderId = t.OrderId,
                            SprintId = newSprint.SprintId,
                            TaskId = t.TaskId
                        });
                    });
                    uow.Save();
                }
                response.SprintId = newSprint.SprintId;
                response.StartDate = newSprint.StartDate;
                response.EndDate = newSprint.EndDate;
                response.TeamId = newSprint.TeamId;
                response.SprintName = newSprint.SprintName;
            });
            return response;
        }

        public SprintResponse GetSprintsList(SprintRequest request)
        {
            var response = new SprintResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var sprints = uow.Repository<SprintEntity>().GetOverview()
                    .Where(b => b.TeamId == request.TeamId)
                    .Select(s => new ShortSprint { SprintName = s.SprintName, SprintId = s.SprintId }).ToList();
                if (sprints != null && sprints.Count > 0)
                {
                    response.SprintsList = sprints;
                    response.Success = true;
                }
                else
                {
                    response.SprintsList = new List<ShortSprint>();
                    response.Errors.Add("Get Sprints", "This team cannot have any sprints yet.");
                }
            });
            return response;
        }

        public UserResponse GetAllUsersWithouUsersInTeam(UserRequest request)
        {
            var response = new UserResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var xRefTeamUsers = uow.Repository<XRefTeamUserEntity>().GetOverview(t => t.TeamId == request.UserTeam.TeamId).Select(x => x.UserId).ToList();
                var users = uow.Repository<UserEntity>().GetOverview().Where(u => !xRefTeamUsers.Contains(u.Id)).Select(u => new User
                {
                    UserId = u.Id,
                    UserName = u.UserName
                }).ToList();
                if (users != null)
                {
                    response.UserList = users;
                }
            });
            return response;
        }

        public TeamResponse GetTeamUsers(TeamRequest request)
        {
            var response = new TeamResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var xRefTeamUser = uow.Repository<XRefTeamUserEntity>().GetOverview();
                var user = uow.Repository<UserEntity>().GetOverview();
                var role = uow.Repository<RoleEntity>().GetOverview();

                var teamUsers = (from x in xRefTeamUser
                                 join u in user
                                 on x.UserId equals u.Id
                                 join r in role
                                 on x.RoleId equals r.RoleId
                                 where x.TeamId == request.TeamId
                                 select new User
                                 {
                                     UserId = u.Id,
                                     UserName = u.UserName,
                                     UserRole = r.RoleName,
                                     Me = request.UserId == x.UserId ? true : false
                                 }).ToList();

                if (teamUsers != null)
                {
                    response.TeamUsers = teamUsers;
                }
            });
            return response;
        }

        public RoleResponse GetUserRoles(RoleRequest request)
        {
            var response = new RoleResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var roles = uow.Repository<RoleEntity>().GetOverview().ToList();
                if (roles != null)
                {
                    response.Roles = _mapper.Map<List<Model.Role>>(roles);
                }
            });
            return response;
        }

        public UserResponse AddUserToTeam(UserRequest request)
        {
            var response = new UserResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                response.Errors = Validator.AddUserToTeam(request.User);
                if (response.Errors.Count() > 0)
                {
                    return;
                }
                uow.Repository<XRefTeamUserEntity>().Add(new XRefTeamUserEntity { RoleId = request.User.RoleId, UserId = request.User.Id, TeamId = request.User.TeamId });
                uow.Save();
                response.TeamId = request.User.TeamId;
            });
            return response;
        }

        public UserResponse DeleteUserFromTeam(UserRequest request)
        {
            var response = new UserResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var xref = uow.Repository<XRefTeamUserEntity>().GetOverview(x => x.UserId == request.UserTeam.UserId && x.TeamId == request.UserTeam.TeamId).FirstOrDefault();
                if (xref != null)
                {
                    uow.Repository<XRefTeamUserEntity>().Delete(xref);
                    uow.Save();
                    response.TeamId = request.UserTeam.TeamId;
                }
            });
            return response;
        }

        public TeamResponse GetTeamById(TeamRequest request)
        {
            var response = new TeamResponse();
            RunCode(MethodBase.GetCurrentMethod().Name, request, response, (uow) =>
            {
                var team = uow.Repository<TeamEntity>().GetOverview(x => x.TeamId == request.TeamId).FirstOrDefault();
                if (team != null)
                {
                    response.Team = _mapper.Map<TeamVM>(team);
                }
            });
            return response;
        }
    }
}