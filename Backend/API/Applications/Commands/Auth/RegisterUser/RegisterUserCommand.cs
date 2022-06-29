using API.Application.Results;
using API.Domain.Entities;
using API.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class RegisterUserCommand : IRequest<UserResponseDto>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public class RegisterUserCommandProfiles : Profile
    {
        public RegisterUserCommandProfiles()
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
}