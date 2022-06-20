using API.Application.Results;
using API.Data.Entities;
using AutoMapper;

namespace API.Application.Profiles;

public class EntityToResult : Profile
{

    public EntityToResult()
    {
        CreateMap<User, UserResponseDto>();
    }
}