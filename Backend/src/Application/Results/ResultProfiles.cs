using API.Domain.Entities;
using AutoMapper;

namespace API.Application.Results.Profiles;

public class ResultProfiles : Profile
{

    public ResultProfiles()
    {
        CreateMap<Project, ProjectResponseDto>();
        CreateMap<Project, ProjectResponseDto>().ReverseMap();
        CreateMap<User, UserResponseDto>();
        CreateMap<Todo, TodoResponseDto>().ReverseMap();
        CreateMap<SubTaskResponseDto, SubTask>().ReverseMap()
            .ForMember(dest =>
                dest.TodoId,
                src => src.MapFrom(it => it.Todo.Id))
            .ForMember(dest =>
                dest.ProjectId,
                src => src.MapFrom(it => it.Todo.Project.Id));
    }
}

