using AutoMapper;
using SecureWebAPI.BusinessLogic.Model;
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
            CreateMap<TeamVM, TeamEntity>();
            CreateMap<TeamEntity, TeamVM>();
            CreateMap<BacklogVM, BacklogEntity>();
            CreateMap<BacklogEntity, BacklogVM>();
            CreateMap<SprintVM, SprintEntity>();
            CreateMap<SprintEntity, SprintVM>();
            CreateMap<RoleEntity, Role>();
            CreateMap<Role, RoleEntity>();
        }
    }
}