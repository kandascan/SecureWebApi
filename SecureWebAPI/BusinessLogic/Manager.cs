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

        public Manager(IUnitOfWork uow, IMapper mapper, UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager, IConfiguration configuration)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
    }
}