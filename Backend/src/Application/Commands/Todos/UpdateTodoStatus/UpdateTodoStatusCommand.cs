using System.ComponentModel.DataAnnotations;
using API.Application.Commands.Dtos;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateTodoStatusCommand : IRequest<TodoResponseDto>
{
    [Required]
    public int? Id { get; set; } = null;

    [Required]
    public int ProjectId { get; set; }

    [Required]
    public ProgressStatus Status { get; set; }


    public class UpdateTodoStatusCommandProfiles : Profile
    {
        public UpdateTodoStatusCommandProfiles()
        {
            CreateMap<UpdateTodoStatusRequestDto, UpdateTodoStatusCommand>();
        }
    }
}