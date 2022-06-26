using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using MediatR;

namespace API.Application.Commands;

public class CreateProjectCommand : IRequest<ProjectResponseDto>
{
    [Required]
    public new string Title { get; set; }

    [Required]
    public new string Description { get; set; }

    [Required]
    public bool IsOrdered { get; set; }
}