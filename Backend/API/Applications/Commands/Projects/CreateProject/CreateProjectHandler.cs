using API.Application.Results;
using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Commands;

public class CreateProjectHandler : BaseHandler<Project>, IRequestHandler<CreateProjectCommand, ProjectResponseDto>
{
    private readonly HttpContext _context;
    public CreateProjectHandler(IMapper mapper, AppDbContext db, IHttpContextAccessor httpContextAccessor)
            : base(mapper, db)
    {
        _context = httpContextAccessor.HttpContext;
    }

    public async Task<ProjectResponseDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var currentUserName = _context.User.Identity.Name;
        var currentUser = await _db.Users.SingleAsync(u => u.Username.Equals(currentUserName));

        var newProject = _mapper.Map<Project>(request);
        newProject.Owner = currentUser;
        newProject.Members = new List<User>() { currentUser };

        var result = _db.Projects.Add(newProject).Entity;
        await _db.SaveChangesAsync();

        return _mapper.Map<ProjectResponseDto>(result);
    }
}
