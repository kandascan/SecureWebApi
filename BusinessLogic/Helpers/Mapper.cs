using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>();
        }
    }
}