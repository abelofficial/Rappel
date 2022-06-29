using API.Domain.Entities;
using API.Infrastructure.Data;
using AutoMapper;

namespace API.Application;

public class BaseHandler<T> where T : IEntity
{

    protected readonly AppDbContext _db;
    protected readonly IMapper _mapper;
    public BaseHandler(IMapper mapper, AppDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }
}