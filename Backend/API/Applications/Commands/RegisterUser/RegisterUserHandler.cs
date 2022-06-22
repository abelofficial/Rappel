using System.Net;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class RegisterUserHandler : BaseHandler<User>, IRequestHandler<RegisterUserCommand, UserResponseDto>
{
    public RegisterUserHandler(IMapper mapper, IRepository<User> repo)
            : base(mapper, repo) { }

    public async Task<UserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var usernameTaken = (await _repo.GetAll(u => u.Username == request.Username)).Any();
        string? errors = null;

        if (usernameTaken)
            errors = "Username is already taken.";

        var emailAddressTaken = (await _repo.GetAll(u => u.Email == request.Email)).Any();

        if (emailAddressTaken)
            errors += "\nEmail address is already taken.";

        if (errors != null) throw new HttpRequestException(errors, null, HttpStatusCode.BadRequest);

        var user = _mapper.Map<User>(request);
        _repo.Create(user);
        await _repo.SaveChanges();
        return _mapper.Map<UserResponseDto>(user);
    }
}