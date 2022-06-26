using System.Net;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Queries;

public class GetUserProjectHandler : BaseHandler<Todo>, IRequestHandler<GetUserProjectQuery, ProjectResponseDto>
{

    private readonly HttpContext _context;

    public GetUserProjectHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<ProjectResponseDto> Handle(GetUserProjectQuery request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        try
        {
            var result = await _db.Projects.SingleAsync(p => p.Id == request.Id && (p.Owner.Id == currentUser.Id || p.Members.Any(m => m.Id == currentUser.Id)));

            return _mapper.Map<ProjectResponseDto>(result);
        }
        catch (Exception)
        {
            throw new HttpRequestException($"Project with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }


}