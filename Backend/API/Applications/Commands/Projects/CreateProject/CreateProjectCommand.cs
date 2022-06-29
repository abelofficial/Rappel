using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateProjectCommand : IRequest<ProjectResponseDto>
{
    [Required]
    public new string Title { get; set; }

    [Required]
    [MinLength(5)]
    public new string Description { get; set; }

    [Required]
    public bool IsOrdered { get; set; }

    public class CreateProjectCommandProfiles : Profile
    {
        public CreateProjectCommandProfiles()
        {
            CreateMap<CreateProjectCommand, Project>();
        }
    }
}