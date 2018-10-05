using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.DataAccess.UnitOfWork;
using SecureWebAPI.Enums;
using SecureWebAPI.Extensions;
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


        public TodoResponse GetTodoById(TodoRequest request)
        {
            var response = new TodoResponse();
            try
            {
                var todo = _uow.Repository<TodoEntity>().GetDetails(x => x.Id == request.TodoId);
                if (todo != null)
                {
                    response.Todo = _mapper.Map<TodoVM>(todo);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Not Exist", "Task not exist in database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<UserResponse> RegisterUser(UserRequest request)
        {
            var response = new UserResponse();
            var user = _mapper.Map<UserEntity>(request.User);
            try
            {
                var result = await _userManager.CreateAsync(user, request.User.Password);
                response.Success = result.Succeeded;
                if (result.Succeeded)
                    response.Token = await user.GenerateJwtToken(_configuration);
                else
                {
                    var error = result.Errors.FirstOrDefault();
                    response.Errors.Add(error.Code, error.Description);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
                _logger.LogError(ex.Message);
            }

            return response;
        }
        public async Task<UserResponse> LoginUser(UserRequest request)
        {
            var response = new UserResponse();
            var userName = _mapper.Map<UserEntity>(request.User).UserName;
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    response.Errors.Add(Errors.NOT_EXIST.ToString(), "User not found");
                else if (!await _userManager.CheckPasswordAsync(user, request.User.Password))
                    response.Errors.Add(Errors.PASSWORD_INCORRECT.ToString(), "User password incorrect");


                var signIn = await _signInManager.PasswordSignInAsync(user, request.User.Password, true, false);
                if (signIn.Succeeded)
                {
                    response.Token = await user.GenerateJwtToken(_configuration);
                }
                response.Success = signIn.Succeeded;
            }
            catch (Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
            }

            return response;
        }

        public TodoResponse GetUserTodos(TodoRequest request)
        {
            var response = new TodoResponse();
            try
            {
                var todos = _uow.Repository<TodoEntity>().GetOverview(u => u.UserId == request.UserId).ToArray();
                if (todos != null)
                {
                    response.Todos = _mapper.Map<List<TodoVM>>(todos);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Get User Todos", "Cannot featch User Todos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public TodoResponse GetAllTodos(TodoRequest request)
        {
            var response = new TodoResponse();
            try
            {
                var todos = _uow.Repository<TodoEntity>().GetOverview();
                if (todos != null)
                {
                    response.Todos = _mapper.Map<List<TodoVM>>(todos);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Get Todos", "Cannot featch Todos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public TodoResponse UpdateTodo(TodoRequest request)
        {
            var response = new TodoResponse();
            try
            {
                var updateTodo = _uow.Repository<TodoEntity>().GetDetails(t => t.Id == request.Todo.Id);
                updateTodo.Name = request.Todo.Name;
                var todo = _uow.Repository<TodoEntity>().Update(updateTodo);
                _uow.Save();

                if (todo != null)
                {
                    response.Todo = _mapper.Map<TodoVM>(todo);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Updated error", "Cannot update task");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public TodoResponse RemoveTodo(TodoRequest request)
        {
            var response = new TodoResponse();
            try
            {
                var todo = _uow.Repository<TodoEntity>().GetDetails(t => t.Id == request.TodoId);
                _uow.Repository<TodoEntity>().Delete(todo);
                _uow.Save();

                response.Success = true;
                response.Message = "Todo removed successfuly";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Errors.Add("System Exception", ex.Message);
            }
            return response;
        }

        public TodoResponse CreateTodo(TodoRequest request)
        {
            var response = new TodoResponse();
            try
            {
                var newTodo = _mapper.Map<TodoEntity>(request.Todo);
                var todo = _uow.Repository<TodoEntity>().Add(newTodo);
                _uow.Save();
                if (todo != null)
                {
                    response.Todo = _mapper.Map<TodoVM>(todo);
                    response.Success = true;
                }
                else
                {
                    response.Errors.Add("Create error", "Cannot create task");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Errors.Add("System Exception", ex.Message);
            }

            return response;
        }
    }
}