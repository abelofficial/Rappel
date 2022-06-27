using API.Application.Queries;
using API.Application.Results;
using API.Data;
using API.Data.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateProjectHandler : BaseHandler<Project>, IRequestHandler<UpdateProjectCommand, ProjectResponseDto>
{
    private readonly IMediator _mediator;
    public UpdateProjectHandler(IMapper mapper, AppDbContext db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<ProjectResponseDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var targetProject = await _mediator.Send(new GetUserProjectQuery() { Id = request.Id });
        var project = await _db.Projects.FindAsync(targetProject.Id);
        _mapper.Map(request, project);
        project.Id = targetProject.Id;
        _db.Projects.Update(project);
        await _db.SaveChangesAsync();
        return _mapper.Map<ProjectResponseDto>(project);

    }
}
