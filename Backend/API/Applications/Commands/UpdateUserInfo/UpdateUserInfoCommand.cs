using System.ComponentModel.DataAnnotations;
using API.Application.Results;
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
}