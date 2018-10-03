using AutoMapper;
using BusinessLogic.Extensions;
using BusinessLogic.Request;
using BusinessLogic.Response;
using DataAccess.Entities;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BusinessLogic.Service
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
            var result =  await _userManager.CreateAsync(user, request.User.Password);
            if (result.Succeeded)
                response.Token = await user.GenerateJwtToken(_configuration);

            return response;
        }
    }
}
