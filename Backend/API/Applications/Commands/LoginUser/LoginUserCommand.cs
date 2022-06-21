using System.ComponentModel.DataAnnotations;
using API.Application.Results;
using MediatR;

namespace API.Application.Commands;

public class LoginUserCommand : IRequest<LoginResponseDto>
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}