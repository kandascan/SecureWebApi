using System;
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

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
        {
            var response = new RegisterUserResponse();
            var user = _mapper.Map<UserEntity>(request.User);
            try{
                var result =  await _userManager.CreateAsync(user, request.User.Password);
                response.Success = result.Succeeded;
                if (result.Succeeded)
                    response.Token = await user.GenerateJwtToken(_configuration);
            }
            catch(Exception ex)
            {
                response.Errors.Add(Errors.EXCEPTION, ex.Message);
            }

            return response;
        }
    }
}