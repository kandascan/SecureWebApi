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
            CreateMap<TaskEntity, TaskVM>();
            CreateMap<TaskVM, TaskEntity>();
            CreateMap<PriorityEntity, PriorityVM>();
            CreateMap<PriorityVM, PriorityEntity>();
            CreateMap<EffortEntity, EffortVM>();
            CreateMap<EffortVM, EffortEntity>();
        }
    }
}