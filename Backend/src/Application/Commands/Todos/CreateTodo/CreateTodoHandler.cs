using API.Application.Queries;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateTodoHandler : BaseHandler<Todo>, IRequestHandler<CreateTodoCommand, TodoResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Project> _projectDb;
    private readonly IRepository<User> _userDb;

    public CreateTodoHandler(IMapper mapper, IRepository<Todo> db, IRepository<Project> projectDb, IRepository<User> userDb, IMediator mediator)
            : base(mapper, db)
    {
        _mediator = mediator;
        _projectDb = projectDb;
        _userDb = userDb;
    }

    public async Task<TodoResponseDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {

        var currentUser = await _mediator.Send(new CurrentUserQuery());
        var project = await _mediator.Send(new GetProjectQuery() { Id = request.ProjectId });

        var newTodoItem = _mapper.Map<Todo>(request);
        newTodoItem.Project = await _projectDb.GetOne(project.Id);
        newTodoItem.Status = ProgressStatus.CREATED;
        newTodoItem.AddOwner(await _userDb.GetOne(u => u.Id == currentUser.Id));
        var result = _db.Create(newTodoItem);
        await _db.SaveChanges();

        return _mapper.Map<TodoResponseDto>(result);
    }
}
