using API.Data.Entities;
using API.Data.Repositories;
using AutoMapper;

namespace API.Application;

public class BaseHandler<T> where T : IEntity
{

    protected readonly IRepository<T> _repo;
    protected readonly IMapper _mapper;
    public BaseHandler(IMapper mapper, IRepository<T> repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
}