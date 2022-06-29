using API.Application.Results;
using MediatR;

namespace API.Application.Commands;

public class RegisterUserCommand : IRequest<UserResponseDto>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}