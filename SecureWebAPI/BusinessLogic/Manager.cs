using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class Manager : IManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;

        public Manager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager, IConfiguration configuration)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public void CreateTask(TaskRequest request, TaskResponse response)
        {
            response.Errors = Validator.CreateTask(request.Task);
            if (response.Errors.Count() > 0)
            {
                return;
            }
            var newTask = _mapper.Map<TaskEntity>(request.Task);
            var task = _uow.Repository<TaskEntity>().Add(newTask);
            _uow.Save();
            if (task != null)
            {
                if (task.Sprint != -1)
                {
                    var xRefSprintTask = new XRefSprintTaskEntity { SprintId = (int)task.Sprint, TaskId = task.Id };
                    _uow.Repository<XRefSprintTaskEntity>().Add(xRefSprintTask);
                    var backlogTaskDb = _uow.Repository<XRefBacklogTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                    if (backlogTaskDb != null)
                    {
                        _uow.Repository<XRefBacklogTaskEntity>().Delete(backlogTaskDb);
                    }
                }
                else
                {
                    var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.Task.TeamId).FirstOrDefault();
                    var xRefBacklogTask = new XRefBacklogTaskEntity { TaskId = task.Id, BacklogId = backlog.BacklogId };
                    _uow.Repository<XRefBacklogTaskEntity>().Add(xRefBacklogTask);
                    var sprintTaskDb = _uow.Repository<XRefSprintTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                    if (sprintTaskDb != null)
                    {
                        _uow.Repository<XRefSprintTaskEntity>().Delete(sprintTaskDb);
                    }
                }
                _uow.Save();
                response.Task = _mapper.Map<TaskVM>(task);
            }
            else
            {
                response.Errors.Add("Create error", "Cannot create task");
            }
        }

        public void GetBacklogTasks(GetBacklogTasksRequest request, GetBacklogTasksResponse response)
        {
            var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.TeamId).FirstOrDefault();
            var xRefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetOverview(x => x.BacklogId == backlog.BacklogId).Select(t => t.TaskId);
            var tasks = _uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
            if (tasks != null && tasks.Count() > 0)
            {
                response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
            }
            else
            {
                response.Errors.Add("Get Tasks", "Cannot featch Tasks");
            }
        }

        private GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request)
        {
            var response = new GetBacklogTasksResponse();
            var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.TeamId).FirstOrDefault();
            var xRefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetOverview(x => x.BacklogId == backlog.BacklogId).Select(t => t.TaskId);
            var tasks = _uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
            if (tasks != null && tasks.Count() > 0)
            {
                response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
            }
            else
            {
                response.Errors.Add("Get Tasks", "Cannot featch Tasks");
            }

            return response;
        }

        public void GetTaskById(TaskRequest request, TaskResponse response)
        {
            var task = _uow.Repository<TaskEntity>().GetOverview(x => x.Id == request.TaskId).FirstOrDefault();
            if (task != null)
            {
                response.Task = _mapper.Map<TaskVM>(task);
            }
            else
            {
                response.Errors.Add("Get Task", "Cannot featch Task");
            }
        }

        public void IsUserTeamMember(BaseRequest request, BaseResponse response)
        {
            var teamMember = _uow.Repository<XRefTeamUserEntity>().GetOverview(u => u.UserId == request.UserId && u.TeamId == request.TeamId).FirstOrDefault();
            if (teamMember != null)
                response.IsTeamMember = true;
        }

        public async Task LoginUser(UserRequest request, UserResponse response)
        {
            response.Errors = Validator.Login(request.User);
            if (response.Errors.Count() > 0)
            {
                return;
            }
            var userName = _mapper.Map<UserEntity>(request.User).UserName;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                response.Errors.Add("username", "User not found");
            else if (!await _userManager.CheckPasswordAsync(user, request.User.Password))
                response.Errors.Add("password", "User password incorrect");
            else
            {
                var signIn = await _signInManager.PasswordSignInAsync(user, request.User.Password, true, false);
                if (signIn.Succeeded)
                {
                    var userTeams = _uow.Repository<XRefTeamUserEntity>().GetOverview(u => u.UserId == user.Id).Select(t => t.TeamId).ToArray();
                    response.Token = await user.GenerateJwtToken(_configuration, userTeams);
                    response.Success = signIn.Succeeded;
                }
            }
        }

        public async Task LogOutUser(UserRequest request, UserResponse response)
        {
            await _signInManager.SignOutAsync();
            response.Message = "User logout";
        }

        public async Task RegisterUser(UserRequest request, UserResponse response)
        {
            response.Errors = Validator.Register(request.User);
            if (response.Errors.Count() > 0)
            {
                return;
            }
            var user = _mapper.Map<UserEntity>(request.User);
            var result = await _userManager.CreateAsync(user, request.User.Password);
            response.Success = result.Succeeded;
            if (result.Succeeded)
                response.Token = await user.GenerateJwtToken(_configuration, new int[0]);
            else
            {
                if (response.Errors.Count() == 0)
                {
                    var errorType = "";
                    var error = result.Errors.FirstOrDefault();
                    if (error.Code.Contains("Password"))
                    {
                        errorType = "password";
                    }
                    else if (error.Code.Contains("User"))
                    {
                        errorType = "username";
                    }
                    else if (error.Code.Contains("Email"))
                    {
                        errorType = "email";
                    }
                    response.Errors.Add(errorType, error.Description);
                }
            }
        }

        public void SortBacklogItems(SortBacklogItemsRequest request, SortBacklogItemsResponse response)
        {
            var ids = request.Items.Select(i => i.Id).ToArray();
            var dbTasks = _uow.Repository<TaskEntity>().GetOverview(i => ids.Contains(i.Id)).OrderBy(o => ids.IndexOf(o.Id)).ToList();

            if (dbTasks != null && dbTasks.Count > 0)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    dbTasks[i].OrderId = i;
                }
                _uow.Save();
                response.Tasks = _mapper.Map<List<TaskVM>>(dbTasks);
            }
            else
            {
                response.Errors.Add("Sort Backlog items", "Cannot featch Backlog items");
            }
        }

        public void GetPriorities(GetPrioritiesRequest request, GetPrioritiesResponse response)
        {
            var dbPriorites = _uow.Repository<PriorityEntity>().GetAll();
            if (dbPriorites != null && dbPriorites.Count() > 0)
            {
                response.Priorities = _mapper.Map<List<PriorityVM>>(dbPriorites);
            }
        }

        public void GetEfforts(GetEffortsRequest request, GetEffortsResponse response)
        {
            var dbEfforts = _uow.Repository<EffortEntity>().GetAll();
            if (dbEfforts != null && dbEfforts.Count() > 0)
            {
                response.Efforts = _mapper.Map<List<EffortVM>>(dbEfforts);
            }
        }

        public void UpdateTask(TaskRequest request, TaskResponse response)
        {
            var editedTask = _mapper.Map<TaskEntity>(request.Task);
            var task = _uow.Repository<TaskEntity>().Update(editedTask);
            if (task != null)
            {
                if (task.Sprint != -1)
                {
                    var xRefSprintTask = new XRefSprintTaskEntity { SprintId = (int)task.Sprint, TaskId = task.Id, OrderId = -1 };
                    var xst = _uow.Repository<XRefSprintTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                    if (xst == null)
                        _uow.Repository<XRefSprintTaskEntity>().Add(xRefSprintTask);
                    var backlogTaskDb = _uow.Repository<XRefBacklogTaskEntity>().GetOverview(t => t.TaskId == editedTask.Id).FirstOrDefault();
                    if (backlogTaskDb != null)
                    {
                        _uow.Repository<XRefBacklogTaskEntity>().Delete(backlogTaskDb);
                    }
                }
                else
                {
                    var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.Task.TeamId).FirstOrDefault();
                    var xRefBacklogTask = new XRefBacklogTaskEntity { TaskId = task.Id, BacklogId = backlog.BacklogId };
                    var xbt = _uow.Repository<XRefBacklogTaskEntity>().GetOverview(t => t.TaskId == task.Id).FirstOrDefault();
                    if (xbt == null)
                        _uow.Repository<XRefBacklogTaskEntity>().Add(xRefBacklogTask);
                    var sprintTaskDb = _uow.Repository<XRefSprintTaskEntity>().GetOverview(t => t.TaskId == editedTask.Id).FirstOrDefault();
                    if (sprintTaskDb != null)
                    {
                        _uow.Repository<XRefSprintTaskEntity>().Delete(sprintTaskDb);
                    }
                }
                _uow.Save();
                response.Task = _mapper.Map<TaskVM>(task);
            }
            else
            {
                response.Errors.Add("Update error", "Cannot update task");
            }
        }

        public async Task CreateTeam(TeamRequest request, TeamResponse response)
        {
            response.Errors = Validator.CreateTeam(request.Team);
            if (response.Errors.Count() > 0)
            {
                return;
            }
            var teamDb = _uow.Repository<TeamEntity>().GetOverview(t => t.TeamName == request.Team.TeamName).FirstOrDefault();
            if(teamDb != null)
            {
                response.Errors.Add("teamname", $"Team with name {request.Team.TeamName} already exist.");
                return;
            }
            var newTeam = _mapper.Map<TeamEntity>(request.Team);
            _uow.Repository<TeamEntity>().Add(newTeam);
            _uow.Save();

            var newXrefTeamUser = new XRefTeamUserEntity { TeamId = newTeam.TeamId, UserId = request.UserId, RoleId = (int)Enums.Role.SCRUM_MASTER };
            _uow.Repository<XRefTeamUserEntity>().Add(newXrefTeamUser);
            _uow.Save();

            var backlog = new BacklogEntity { TeamId = newTeam.TeamId };
            _uow.Repository<BacklogEntity>().Add(backlog);
            _uow.Save();
            var user = _uow.Repository<UserEntity>().GetOverview(u => u.Id == request.UserId).FirstOrDefault();
            var userTeams = _uow.Repository<XRefTeamUserEntity>().GetOverview(u => u.UserId == user.Id).Select(t => t.TeamId).ToArray();
            response.Token = await user.GenerateJwtToken(_configuration, userTeams);

            response.Team = _mapper.Map<TeamVM>(newTeam);
        }

        public void GetUserTeams(TeamRequest request, TeamResponse response)
        {
            var teams = _uow.Repository<TeamEntity>().GetOverview();
            var xrefTeamsUsers = _uow.Repository<XRefTeamUserEntity>().GetOverview();
            var userRoles = _uow.Repository<RoleEntity>().GetOverview();
            var backlog = _uow.Repository<BacklogEntity>().GetOverview();
            var xrefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetOverview();
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
        }

        public void GetTeamBacklog(BacklogRequest request, BacklogResponse response)
        {
            var teamBacklog = _uow.Repository<BacklogEntity>().GetOverview().Where(b => b.TeamId == request.TeamId).FirstOrDefault();
            if (teamBacklog != null)
            {
                var xRefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetOverview().Where(x => x.BacklogId == teamBacklog.BacklogId).Select(i => i.TaskId);
                var tasks = _uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
                if (tasks != null)
                {
                    response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
                }
            }
            else
            {
                response.Errors.Add("Get Tasks", "Cannot featch Tasks");
            }
        }

        public void SortSprintTasks(SprintRequest request, SprintResponse response)
        {
            var sprintColumn = _uow.Repository<SprintColumnEntity>().GetOverview();
            var xRefSprintTask = _uow.Repository<XRefSprintTaskEntity>().GetOverview();
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
                    _uow.Repository<XRefSprintTaskEntity>().Update(xst);
                    orderId++;
                }
            }
            _uow.Save();
        }

        public void CreateSprint(SprintRequest request, SprintResponse response)
        {
            var oldSprint = request.Sprint.SprintId;
            int sprintNumber = 1;
            string sprintName = string.Empty;
            var teamName = _uow.Repository<TeamEntity>().GetOverview()
                   .Where(t => t.TeamId == request.Sprint.TeamId)
                   .Select(n => n.TeamName).FirstOrDefault();
            if (!string.IsNullOrEmpty(teamName))
            {
                sprintName = $"{teamName} - Sprint: {sprintNumber}";
            }

            var teamSprints = _uow.Repository<SprintEntity>().GetOverview()
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
            _uow.Repository<SprintEntity>().Add(newSprint);
            _uow.Save();

            if (oldSprint != 0)
            {
                var notCompletedTasks = _uow.Repository<XRefSprintTaskEntity>().GetOverview(s => s.SprintId == oldSprint && s.ColumnId != (int)Column.DONE).ToList();
                notCompletedTasks.ForEach(t =>
                {
                    _uow.Repository<XRefSprintTaskEntity>().Add(new XRefSprintTaskEntity
                    {
                        ColumnId = t.ColumnId,
                        OrderId = t.OrderId,
                        SprintId = newSprint.SprintId,
                        TaskId = t.TaskId
                    });
                });
                _uow.Save();
            }
            response.SprintId = newSprint.SprintId;
            response.StartDate = newSprint.StartDate;
            response.EndDate = newSprint.EndDate;
            response.TeamId = newSprint.TeamId;
            response.SprintName = newSprint.SprintName;
        }

        public void GetSprintsList(SprintRequest request, SprintResponse response)
        {
            var sprints = _uow.Repository<SprintEntity>().GetOverview()
                    .Where(b => b.TeamId == request.TeamId)
                    .Select(s => new ShortSprint { SprintName = s.SprintName, SprintId = s.SprintId }).ToList();
            if (sprints != null && sprints.Count > 0)
            {
                response.SprintsList = sprints;
            }
            else
            {
                response.SprintsList = new List<ShortSprint>();
                response.Errors.Add("Get Sprints", "This team cannot have any sprints yet.");
            }
        }

        public void GetAllUsersWithoutUsersInTeam(UserRequest request, UserResponse response)
        {
            var xRefTeamUsers = _uow.Repository<XRefTeamUserEntity>().GetOverview(t => t.TeamId == request.UserTeam.TeamId).Select(x => x.UserId).ToList();
            var users = _uow.Repository<UserEntity>().GetOverview().Where(u => !xRefTeamUsers.Contains(u.Id)).Select(u => new User
            {
                UserId = u.Id,
                UserName = u.UserName
            }).ToList();
            if (users != null)
            {
                response.UserList = users;
            }
        }

        public void GetTeamUsers(TeamRequest request, TeamResponse response)
        {
            var xRefTeamUser = _uow.Repository<XRefTeamUserEntity>().GetOverview();
            var user = _uow.Repository<UserEntity>().GetOverview();
            var role = _uow.Repository<RoleEntity>().GetOverview();
            var tasks = _uow.Repository<TaskEntity>().GetOverview();

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
                                 Me = request.UserId == x.UserId ? true : false,
                                 CountTasks = tasks.Where(t => t.UserId == u.Id && t.TeamId == request.TeamId).Count()
                             }).ToList();

            if (teamUsers != null)
            {
                response.TeamUsers = teamUsers;
            }
        }

        public void GetUserRoles(RoleRequest request, RoleResponse response)
        {
            var roles = _uow.Repository<RoleEntity>().GetOverview().ToList();
            if (roles != null)
            {
                response.Roles = _mapper.Map<List<Model.Role>>(roles);
            }
        }

        public void AddUserToTeam(UserRequest request, UserResponse response)
        {
            response.Errors = Validator.AddUserToTeam(request.User);
            if (response.Errors.Count() > 0)
            {
                return;
            }
            _uow.Repository<XRefTeamUserEntity>().Add(new XRefTeamUserEntity { RoleId = request.User.RoleId, UserId = request.User.Id, TeamId = request.User.TeamId });
            _uow.Save();
            response.TeamId = request.User.TeamId;
        }

        public void DeleteUserFromTeam(UserRequest request, UserResponse response)
        {
            var xref = _uow.Repository<XRefTeamUserEntity>().GetOverview(x => x.UserId == request.UserTeam.UserId && x.TeamId == request.UserTeam.TeamId).FirstOrDefault();
            if (xref != null)
            {
                _uow.Repository<XRefTeamUserEntity>().Delete(xref);
                var tasks = _uow.Repository<TaskEntity>().GetOverview(u => u.UserId == request.UserTeam.UserId).ToList();
                if (tasks.Count > 0)
                {
                    tasks.ForEach(t => t.UserId = null);
                    _uow.Repository<TaskEntity>().UpdateMany(tasks);
                }
                _uow.Save();
                response.TeamId = request.UserTeam.TeamId;
            }
        }

        public void GetTeamById(TeamRequest request, TeamResponse response)
        {
            var team = _uow.Repository<TeamEntity>().GetOverview(x => x.TeamId == request.TeamId).FirstOrDefault();
            if (team != null)
            {
                response.Team = _mapper.Map<TeamVM>(team);
            }
        }

        public async Task RemoveTask(RemoveTaskRequest request, RemoveTaskResponse response)
        {
            await Task.Run(() =>
            {
                var dbTask = _uow.Repository<TaskEntity>().GetDetails(t => t.Id == request.Id);
                if (dbTask != null)
                {
                    var dbXrefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetDetails(x => x.TaskId == request.Id);
                    if (dbXrefBacklogTask != null)
                    {
                        _uow.Repository<XRefBacklogTaskEntity>().Delete(dbXrefBacklogTask);
                    }

                    var dbXRefSprintTasks = _uow.Repository<XRefSprintTaskEntity>().GetOverview(x => x.TaskId == request.Id);
                    if (dbXRefSprintTasks != null && dbXRefSprintTasks.Count() > 0)
                    {
                        _uow.Repository<XRefSprintTaskEntity>().DeleteMany(dbXRefSprintTasks);
                    }

                    _uow.Repository<TaskEntity>().Delete(dbTask);

                    _uow.Save();
                    var backlogItems = GetBacklogTasks(new GetBacklogTasksRequest { TeamId = dbTask.TeamId });
                    if (backlogItems != null && backlogItems.Tasks.Count > 0)
                    {
                        response.Tasks = backlogItems.Tasks;
                    }
                }
                else
                {
                    response.Errors.Add("Sort Backlog items", "Cannot featch Backlog items");
                }
            });
        }

        public async Task GetCurrentSprint(SprintRequest request, SprintResponse response)
        {
            await Task.Run(() =>
            {
                var sprint = _uow.Repository<SprintEntity>().GetOverview().Where(b => b.TeamId == request.TeamId).OrderByDescending(s => s.SprintId).FirstOrDefault();
                var sprintColumn = _uow.Repository<SprintColumnEntity>().GetOverview();
                var xRefSprintTask = _uow.Repository<XRefSprintTaskEntity>().GetOverview();
                var task = _uow.Repository<TaskEntity>().GetOverview();
                var user = _uow.Repository<UserEntity>().GetOverview();
                var effort = _uow.Repository<EffortEntity>().GetOverview();
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
        }
    }
}