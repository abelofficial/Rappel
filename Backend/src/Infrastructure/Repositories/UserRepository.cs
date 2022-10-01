using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Infrastructure.Repository;
public class UserRepository<T> : Repository<T> where T : User
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet
            .Include(st => st.TodoItems)
            .Where(filterExp)
            .ToListAsync();
    }

}