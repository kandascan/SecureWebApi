using AutoMapper;
using SecureWebAPI.Models;

namespace SecureWebAPI.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
            CreateMap<Todo, TodoVM>();
            CreateMap<TodoVM, Todo>();
        }
    }
}