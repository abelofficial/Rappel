using System.Net;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Queries;

public class GetProjectHandler : BaseHandler<Project>, IRequestHandler<GetProjectQuery, ProjectResponseDto>
{

    private readonly IMediator _mediator;

    public GetProjectHandler(IMapper mapper, IRepository<Project> db, IMediator mediator) : base(mapper, db)
    {
        _mediator = mediator;
    }

    public async Task<ProjectResponseDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new CurrentUserQuery());

        try
        {
            var result = await _db
              .GetOne(p => (p.Owner.Id == currentUser.Id ||
                  p.Members.Any(m => m.Id == currentUser.Id)) &&
                p.Id == request.Id);

            return _mapper.Map<ProjectResponseDto>(result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            throw new HttpRequestException($"Project with the id {request.Id} doesn't exist", null, HttpStatusCode.NotFound);
        }
    }

}