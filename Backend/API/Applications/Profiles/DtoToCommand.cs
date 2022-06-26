using API.Application.Commands;
using API.Application.Dtos;
using API.Application.Dtos.CommandsDtos;
using AutoMapper;

namespace API.Application.Profiles;

public class DtoToCommand : Profile
{

    public DtoToCommand()
    {
        CreateMap<UpdateTodoStatusRequestDto, UpdateTodoStatusCommand>();
        CreateMap<UpdateSubtaskStatusRequestDto, UpdateSubtaskStatusCommand>();
        CreateMap<CreateTodoRequestDto, CreateTodoCommand>();
    }
}

