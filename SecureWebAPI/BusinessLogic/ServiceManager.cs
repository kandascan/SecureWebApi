using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.DataAccess.UnitOfWork;
using SecureWebAPI.Enums;
using SecureWebAPI.Extensions;

namespace SecureWebAPI.BusinessLogic
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;

        public ServiceManager(IUnitOfWork uow, IMapper mapper, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public TodoResponse GetTodoById(TodoRequest request)
        {
            var response = new TodoResponse();
            var todo = _uow.Repository<TodoEntity>().GetDetails(x => x.Id == request.TodoId);
            if (todo != null)
            {
                //response.Todo = _mapper.Map<TodoVM>(todo);
            }

            return response;
        }

        public async Task<UserResponse> RegisterUser(UserRequest request)
        {
            var response = new UserResponse();
            var user = _mapper.Map<UserEntity>(request.User);
            try{
                var result =  await _userManager.CreateAsync(user, request.User.Password);
                response.Success = result.Succeeded;
                if (result.Succeeded)
                    response.Token = await user.GenerateJwtToken(_configuration);
                else
                {
                    var error = result.Errors.FirstOrDefault();                    
                    response.Errors.Add(error.Code, error.Description);
                }
            }
            catch(Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
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
            catch(Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
            }
            
            return response;
        }
    }
}