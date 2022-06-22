using System.Net;
using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateUserInfoHandler : BaseHandler<User>, IRequestHandler<UpdateUserInfoCommand, UserResponseDto>
{
    public readonly HttpContext _context;
    public UpdateUserInfoHandler(IMapper mapper, IRepository<User> repo, IHttpContextAccessor httpContextAccessor)
            : base(mapper, repo)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<UserResponseDto> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var targetUser = await _repo.GetOne(u => u.Username.Equals(currentUserName));

        var emailAddressTaken = (await _repo.GetAll(u => targetUser.Id != u.Id && u.Email == request.Email)).Any();

        if (emailAddressTaken)
            throw new HttpRequestException("Email address is already taken.", null, HttpStatusCode.BadRequest);

        _mapper.Map(request, targetUser);
        var result = _repo.Update(targetUser);
        await _repo.SaveChanges();

        return _mapper.Map<UserResponseDto>(result);
    }


}