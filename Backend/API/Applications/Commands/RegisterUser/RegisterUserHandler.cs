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

        if (usernameTaken)
            throw new HttpRequestException("Username is already taken.", null, HttpStatusCode.BadRequest);

        var emailAddressTaken = (await _repo.GetAll(u => u.Email == request.Email)).Any();

        if (emailAddressTaken)
            throw new HttpRequestException("Email address is already taken.", null, HttpStatusCode.BadRequest);

        var user = _mapper.Map<User>(request);
        _repo.Create(user);
        await _repo.SaveChanges();
        return _mapper.Map<UserResponseDto>(user);
    }
}