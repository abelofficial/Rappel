using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class RegisterUserHandler : BaseHandler<User>, IRequestHandler<RegisterUserCommand, UserResponseDto>
{
    public RegisterUserHandler(IMapper mapper, IRepository<User> db)
            : base(mapper, db) { }

    public async Task<UserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var usernameTaken = (await _db.IsTrue(u => u.Username == request.Username));
        string? errors = null;

        if (usernameTaken)
            errors = "Username is already taken.\n";

        var emailAddressTaken = (await _db.IsTrue(u => u.Email == request.Email));

        if (emailAddressTaken)
            errors += "Email address is already taken.";

        if (errors != null) throw new HttpRequestException(errors, null, HttpStatusCode.BadRequest);

        var user = _mapper.Map<User>(request);
        _db.Create(user);
        await _db.SaveChanges();
        return _mapper.Map<UserResponseDto>(user);
    }
}