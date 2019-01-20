using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public ServiceManager(IUnitOfWork uow, IMapper mapper, UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager, IConfiguration configuration, ILoggerManager logger)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<UserResponse> RegisterUser(UserRequest request)
        {
            var response = new UserResponse();
            response.Errors = Validator.Register(request.User);
            if (response.Errors.Count() > 0)
            {
                return response;
            }
            var user = _mapper.Map<UserEntity>(request.User);
            try
            {
                var result = await _userManager.CreateAsync(user, request.User.Password);
                response.Success = result.Succeeded;
                if (result.Succeeded)
                    response.Token = await user.GenerateJwtToken(_configuration);
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
            catch (Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
                _logger.LogError(ex.Message + ex.StackTrace);
            }

            return response;
        }

        public async Task<UserResponse> LoginUser(UserRequest request)
        {
            var response = new UserResponse();
            response.Errors = Validator.Login(request.User);
            if (response.Errors.Count() > 0)
            {
                return response;
            }
            var userName = _mapper.Map<UserEntity>(request.User).UserName;
            try
            {
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
                        response.Token = await user.GenerateJwtToken(_configuration);
                        response.Success = signIn.Succeeded;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
                _logger.LogError(ex.Message + ex.StackTrace);
            }

            return response;
        }

        public async Task<UserResponse> LogOutUser(UserRequest request)
        {
            var response = new UserResponse();
            try
            {
                await _signInManager.SignOutAsync();
                response.Message = "User logout";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
                _logger.LogError(ex.Message + ex.StackTrace);
            }

            return response;
        }

        public TaskResponse CreateTask(TaskRequest request)
        {
            var response = new TaskResponse();
            response.Errors = Validator.CreateTask(request.Task);
            if (response.Errors.Count() > 0)
            {
                return response;
            }
            try
            {
                var newTask = _mapper.Map<TaskEntity>(request.Task);               
                var task = _uow.Repository<TaskEntity>().Add(newTask);
                _uow.Save();
                if (task != null)
                {
                    if (task.Sprint != -1)
                    {
                        var xRefSprintTask = new XRefSprintTaskEntity { SprintId = (int)task.Sprint, TaskId = task.Id };
                    }
                    else
                    {
                        var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.Task.TeamId).FirstOrDefault();
                        var xRefBacklogTask = new XRefBacklogTaskEntity { TaskId = task.Id, BacklogId = backlog.BacklogId };
                        _uow.Repository<XRefBacklogTaskEntity>().Add(xRefBacklogTask);
                        _uow.Save();
                    }

                    response.Task = _mapper.Map<TaskVM>(task);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Create error", "Cannot create task");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }

            return response;
        }

        public TaskResponse GetTaskById(TaskRequest request)
        {
            var response = new TaskResponse();
            try
            {
                var task = _uow.Repository<TaskEntity>().GetOverview(x => x.Id == request.TaskId).FirstOrDefault();
                if (task != null)
                {
                    response.Task = _mapper.Map<TaskVM>(task);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Get Task", "Cannot featch Task");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public GetBacklogTasksResponse GetBacklogTasks(GetBacklogTasksRequest request)
        {
            var response = new GetBacklogTasksResponse();
            try
            {
                var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.TeamId).FirstOrDefault();

                var xRefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetOverview(x => x.BacklogId == backlog.BacklogId).Select(t => t.TaskId);

                var tasks = _uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
                if (tasks != null)
                {
                    response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Get Tasks", "Cannot featch Tasks");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public SortBacklogItemsResponse SortBacklogItems(SortBacklogItemsRequest request)
        {
            var response = new SortBacklogItemsResponse();
            try
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
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Sort Backlog items", "Cannot featch Backlog items");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public RemoveTaskResponse RemoveTask(RemoveTaskRequest request)
        {
            var response = new RemoveTaskResponse();
            try
            {
                var dbTask = _uow.Repository<TaskEntity>().GetDetails(t => t.Id == request.Id);
                if (dbTask != null)
                {
                    _uow.Repository<TaskEntity>().Delete(dbTask);

                    _uow.Save();
                    var backlogItems = GetBacklogTasks(new GetBacklogTasksRequest { TeamId = dbTask.TeamId });
                    if (backlogItems != null && backlogItems.Tasks.Count > 0)
                    {
                        response.Tasks = backlogItems.Tasks;
                    }
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Sort Backlog items", "Cannot featch Backlog items");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }

            return response;
        }

        public GetPrioritiesResponse GetPriorities(GetPrioritiesRequest request)
        {
            var response = new GetPrioritiesResponse();
            try
            {
                var dbPriorites = _uow.Repository<PriorityEntity>().GetAll();
                if (dbPriorites != null && dbPriorites.Count() > 0)
                {
                    response.Priorities = _mapper.Map<List<PriorityVM>>(dbPriorites);
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public GetEffortsResponse GetEfforts(GetEffortsRequest request)
        {
            var response = new GetEffortsResponse();
            try
            {
                var dbEfforts = _uow.Repository<EffortEntity>().GetAll();
                if (dbEfforts != null && dbEfforts.Count() > 0)
                {
                    response.Efforts = _mapper.Map<List<EffortVM>>(dbEfforts);
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public TaskResponse UpdateTask(TaskRequest request)
        {
            var response = new TaskResponse();
            try
            {
                var editedTask = _mapper.Map<TaskEntity>(request.Task);
                var task = _uow.Repository<TaskEntity>().Update(editedTask);
                _uow.Save();
                if (task != null)
                {
                    if (task.Sprint != -1)
                    {
                        var xRefSprintTask = new XRefSprintTaskEntity { SprintId = (int)task.Sprint, TaskId = task.Id };
                    }
                    else
                    {
                        var backlog = _uow.Repository<BacklogEntity>().GetOverview(b => b.TeamId == request.Task.TeamId).FirstOrDefault();
                        var xRefBacklogTask = new XRefBacklogTaskEntity { TaskId = task.Id, BacklogId = backlog.BacklogId };
                        _uow.Repository<XRefBacklogTaskEntity>().Add(xRefBacklogTask);
                        _uow.Save();
                    }

                    response.Task = _mapper.Map<TaskVM>(task);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Update error", "Cannot update task");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }

            return response;
        }

        public TeamResponse CreateTeam(TeamRequest request)
        {
            var response = new TeamResponse();
            response.Errors = Validator.CreateTeam(request.Team);
            if (response.Errors.Count() > 0)
            {
                return response;
            }
            try
            {
                var newTeam = _mapper.Map<TeamEntity>(request.Team);
                _uow.Repository<TeamEntity>().Add(newTeam);
                _uow.Save();

                var newXrefTeamUser = new XRefTeamUserEntity { TeamId = newTeam.TeamId, UserId = request.UserId, RoleId = (int)Role.SCRUM_MASTER };
                _uow.Repository<XRefTeamUserEntity>().Add(newXrefTeamUser);
                _uow.Save();

                var backlog = new BacklogEntity { TeamId = newTeam.TeamId };
                _uow.Repository<BacklogEntity>().Add(backlog);
                _uow.Save();

                response.Team = _mapper.Map<TeamVM>(newTeam);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public TeamResponse GetUserTeams(TeamRequest request)
        {
            var response = new TeamResponse();
            try
            {
                var teams = _uow.Repository<TeamEntity>().GetOverview();
                var xrefTeamsUsers = _uow.Repository<XRefTeamUserEntity>().GetOverview();
                var userRoles = _uow.Repository<RoleEntity>().GetOverview();
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
                                     ScrumMasterUser = r.RoleId == (int)Role.SCRUM_MASTER ? true : false
                                 }).OrderByDescending(t => t.TeamId).ToList();

                if (userTeams != null && userTeams.Count() > 0)
                {
                    response.UserTeams = userTeams;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public BacklogResponse GetTeamBacklog(BacklogRequest request)
        {
            var response = new BacklogResponse();
            try
            {
                var teamBacklog = _uow.Repository<BacklogEntity>().GetOverview().Where(b => b.TeamId == request.TeamId).FirstOrDefault();
                if(teamBacklog != null)
                {
                    var xRefBacklogTask = _uow.Repository<XRefBacklogTaskEntity>().GetOverview().Where(x => x.BacklogId == teamBacklog.BacklogId).Select(i => i.TaskId);
                    var tasks = _uow.Repository<TaskEntity>().GetOverview().Where(t => xRefBacklogTask.Contains(t.Id)).OrderBy(t => t.OrderId).ThenByDescending(t => t.Id);
                    if (tasks != null)
                    {
                        response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
                        response.Success = true;
                    }
                }
                
                else
                {
                    response.Errors.Add("Get Tasks", "Cannot featch Tasks");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public SprintResponse GetCurrentSprint(SprintRequest request)
        {
            var response = new SprintResponse();
            try
            {
                var sprint = _uow.Repository<SprintEntity>().GetOverview().Where(b => b.TeamId == request.TeamId).FirstOrDefault();
                if (sprint != null)
                {
                    response.SprintId = sprint.SprintId;
                    var tasks = _uow.Repository<TaskEntity>().GetOverview().Where(t => t.Sprint == sprint.SprintId).ToList();
                    if (tasks != null)
                    {
                        response.Tasks = _mapper.Map<List<TaskVM>>(tasks);
                        response.Success = true;
                    }
                    else
                    {
                        response.Errors.Add("Get Tasks", "There is no task in current sprint, back to backlog.");
                    }
                }
                else
                {
                    response.Errors.Add("Get Sprint", "There is no active sprint, create sprint first.");
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public SprintResponse CreateSprint(SprintRequest request)
        {
            var response = new SprintResponse();

            try
            {
                var newSprint = _mapper.Map<SprintEntity>(request.Sprint);
                newSprint.StartDate = DateTime.Now;
                newSprint.EndDate = DateTime.Now.AddDays(14);
                _uow.Repository<SprintEntity>().Add(newSprint);
                _uow.Save();
                response.StartDate = newSprint.StartDate;
                response.EndDate = newSprint.EndDate;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }
    }
}