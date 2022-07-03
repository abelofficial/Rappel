using API.Domain.Entities;
using AutoMapper;

namespace API.Application;

public class BaseHandler<T> where T : IEntity
{

    protected readonly IRepository<T> _db;
    protected readonly IMapper _mapper;
    public BaseHandler(IMapper mapper, IRepository<T> db)
    {
        _mapper = mapper;
        _db = db;
    }
}