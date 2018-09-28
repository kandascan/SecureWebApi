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
        }
    }
}