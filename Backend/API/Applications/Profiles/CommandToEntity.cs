using API.Application.Commands;
using API.Data.Entities;
using API.Data.ValueObjects;
using AutoMapper;

namespace API.Application.Profiles;

public class CommandToEntity : Profile
{

    public CommandToEntity()
    {
        CreateMap<RegisterUserCommand, User>()
        .AfterMap((src, dest) =>
        {
            var password = new Password(src.Password);
            dest.PasswordHash = password.PasswordHash;
            dest.PasswordSalt = password.PasswordSalt;
        });
    }
}