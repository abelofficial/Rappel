using System.Net;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class UpdateUserInfoHandler : BaseHandler<User>, IRequestHandler<UpdateUserInfoCommand, UserResponseDto>
{
    public readonly HttpContext _context;
    public UpdateUserInfoHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<UserResponseDto> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var targetUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        var emailAddressTaken = (await _db.Users.AnyAsync(u => targetUser.Id != u.Id && u.Email == request.Email));

        if (emailAddressTaken)
            throw new HttpRequestException("Email address is already taken.", null, HttpStatusCode.BadRequest);

        _mapper.Map(request, targetUser);
        var result = _db.Users.Update(targetUser);
        await _db.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(result);
    }


}