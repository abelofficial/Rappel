using API.Application.Results.Profiles;
using AutoMapper;
using static API.Application.Commands.RegisterUserCommand;

namespace Application.Tests;

public class BaseHandlerTest
{
    public readonly IMapper _mapper;
    public BaseHandlerTest()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile(new ResultProfiles());
            c.AddProfile(new RegisterUserCommandProfiles());

        });
        _mapper = mapperConfig.CreateMapper();
    }
}