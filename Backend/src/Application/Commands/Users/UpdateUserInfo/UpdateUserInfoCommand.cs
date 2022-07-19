using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateUserInfoCommand : IRequest<UserResponseDto>
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }

    public class UpdateUserInfoCommandProfiles : Profile
    {
        public UpdateUserInfoCommandProfiles()
        {
            CreateMap<UpdateUserInfoCommand, User>();
        }
    }
}