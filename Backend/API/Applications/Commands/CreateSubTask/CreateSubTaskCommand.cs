using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using MediatR;

namespace API.Application.Commands;

public class CreateSubTaskCommand : IRequest<SubTaskResponseDto>
{
    [Required]
    public new string Title { get; set; }

    [Required]
    public new string Description { get; set; }

    [Required]
    public int ParentId { get; set; }
}