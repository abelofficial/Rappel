using System.ComponentModel.DataAnnotations;
using API.Application.Commands.Dtos;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateSubTaskCommand : IRequest<SubTaskResponseDto>
{
    [Required]
    public int ProjectId { get; set; }

    [Required]
    public new string Title { get; set; }

    [Required]
    public new string Description { get; set; }

    [Required]
    public int ParentId { get; set; }

    public class CreateSubTaskCommandProfiles : Profile
    {
        public CreateSubTaskCommandProfiles()
        {
            CreateMap<CreateSubTaskCommand, SubTask>();
            CreateMap<CreateSubtaskRequestDto, CreateSubTaskCommand>();
        }
    }
}