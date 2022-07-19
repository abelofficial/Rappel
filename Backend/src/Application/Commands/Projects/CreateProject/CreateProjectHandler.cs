using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateProjectHandler : BaseHandler<Project>, IRequestHandler<CreateProjectCommand, ProjectResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IRepository<User> _userDb;
    public CreateProjectHandler(IMapper mapper, IRepository<Project> db, IRepository<User> userDb, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
        _userDb = userDb;
    }

    public async Task<ProjectResponseDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());

        var newProject = _mapper.Map<Project>(request);
        newProject.Owner = await _userDb.GetOne(u => u.Id == currentUser.Id);
        newProject.Members = new List<User>() { newProject.Owner };

        var result = _db.Create(newProject);
        await _db.SaveChanges();

        return _mapper.Map<ProjectResponseDto>(result);
    }
}
