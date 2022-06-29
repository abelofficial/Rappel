using System.ComponentModel.DataAnnotations;
using API.Application.Commands.Dtos;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateProjectCommand : IRequest<ProjectResponseDto>
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool IsOrdered { get; set; }

    public class UpdateProjectCommandProfiles : Profile
    {
        public UpdateProjectCommandProfiles()
        {
            CreateMap<UpdateSubtaskCommand, SubTask>();
            CreateMap<UpdateProjectRequestDto, UpdateProjectCommand>();
        }
    }
}