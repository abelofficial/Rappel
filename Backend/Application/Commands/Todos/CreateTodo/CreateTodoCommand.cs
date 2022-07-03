using System.ComponentModel.DataAnnotations;
using API.Application.Commands.Dtos;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateTodoCommand : IRequest<TodoResponseDto>
{
    [Required]
    public int ProjectId { get; set; }

    [Required]
    public string Title { get; set; }
    [Required]
    [MinLength(20)]
    public string Description { get; set; }

    public class CreateTodoCommandProfiles : Profile
    {
        public CreateTodoCommandProfiles()
        {
            CreateMap<CreateTodoCommand, Todo>();
            CreateMap<CreateTodoRequestDto, CreateTodoCommand>();
        }
    }
}