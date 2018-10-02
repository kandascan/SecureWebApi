using AutoMapper;
using DataAccess.Entities;
using SecureWebAPI.Models;

namespace BusinessLogic.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserEntity, UserVM>();
            CreateMap<UserVM, UserEntity>();
            CreateMap<TodoEntity, TodoVM>();
            CreateMap<TodoVM, TodoEntity>();
        }
    }
}