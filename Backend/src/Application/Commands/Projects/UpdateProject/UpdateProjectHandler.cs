using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class UpdateProjectHandler : BaseHandler<Project>, IRequestHandler<UpdateProjectCommand, ProjectResponseDto>
{
    private readonly IMediator _mediator;
    public UpdateProjectHandler(IMapper mapper, IRepository<Project> db, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<ProjectResponseDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var targetProject = await _mediator.Send(new GetProjectQuery() { Id = request.Id });
        var project = await _db.GetOne(targetProject.Id);
        _mapper.Map(request, project);
        project.Id = targetProject.Id;
        _db.Update(project);
        await _db.SaveChanges();
        return _mapper.Map<ProjectResponseDto>(project);

    }
}
