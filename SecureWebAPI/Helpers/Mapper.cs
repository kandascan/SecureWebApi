using AutoMapper;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.Models;

namespace SecureWebAPI.Helpers
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