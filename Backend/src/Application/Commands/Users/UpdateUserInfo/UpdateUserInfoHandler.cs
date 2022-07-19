using System.Net;
using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateUserInfoHandler : BaseHandler<User>, IRequestHandler<UpdateUserInfoCommand, UserResponseDto>
{
    public readonly IMediator _mediator;
    public UpdateUserInfoHandler(IMapper mapper, IRepository<User> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<UserResponseDto> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {

        var currentUer = await _mediator.Send(new CurrentUserQuery());

        var emailAddressTaken = (await _db.IsTrue(u => currentUer.Id != u.Id && u.Email == request.Email));

        if (emailAddressTaken)
            throw new HttpRequestException("Email address is already taken.", null, HttpStatusCode.BadRequest);

        var user = await _db.GetOne(u => u.Id == currentUer.Id);
        _mapper.Map(request, currentUer);
        var result = _db.Update(user);
        await _db.SaveChanges();

        return _mapper.Map<UserResponseDto>(result);
    }


}