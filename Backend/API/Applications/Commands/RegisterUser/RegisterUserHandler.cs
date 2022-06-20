using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class RegisterUserHandler : BaseHandler<User>, IRequestHandler<RegisterUserCommand>
{
    public RegisterUserHandler(IMapper mapper, IRepository<User> repo)
            : base(mapper, repo) { }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        return await Task.FromResult(Unit.Value);
    }
}