using API.Application.Results;
using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;

public class CreateSubTaskHandler : BaseHandler<SubTask>, IRequestHandler<CreateSubTaskCommand, SubTaskResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Todo> _todoRepo;

    public CreateSubTaskHandler(IMapper mapper, IRepository<SubTask> repo, IRepository<Todo> todoRepo, IMediator mediator)
            : base(mapper, repo)
    {
        _mediator = mediator;
        _todoRepo = todoRepo;
    }

    public async Task<SubTaskResponseDto> Handle(CreateSubTaskCommand request, CancellationToken cancellationToken)
    {
        var newTodoItem = _mapper.Map<SubTask>(request);
        newTodoItem.Todo = await _todoRepo.GetOne(request.ParentId);

        var result = _repo.Create(newTodoItem);
        await _repo.SaveChanges();

        return _mapper.Map<SubTaskResponseDto>(result);
    }
}
