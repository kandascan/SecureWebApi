using AutoMapper;
using BusinessLogic.Request;
using BusinessLogic.Response;
using DataAccess.Entities;
using DataAccess.UnitOfWork;
using SecureWebAPI.Models;

namespace BusinessLogic.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ServiceManager(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public TodoResponse GetTodoById(TodoRequest request)
        {
            var response = new TodoResponse();
            var todo = _uow.Repository<TodoEntity>().GetDetails(x => x.Id == request.TodoId);
            if (todo != null)
            {
                response.Todo = _mapper.Map<TodoVM>(todo);
            }

            return response;
        }
    }
}
