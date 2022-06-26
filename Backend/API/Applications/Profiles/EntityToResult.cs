using API.Application.Results;
using API.Data.Entities;
using AutoMapper;

namespace API.Application.Profiles;

public class EntityToResult : Profile
{

    public EntityToResult()
    {
        CreateMap<Project, ProjectResponseDto>();
        CreateMap<User, UserResponseDto>();
        CreateMap<Todo, TodoResponseDto>().ReverseMap();
        CreateMap<SubTaskResponseDto, SubTask>().ReverseMap()
       .ForMember(dest =>
            dest.TodoId,
            src => src.MapFrom(it => it.Todo.Id));
    }
}

