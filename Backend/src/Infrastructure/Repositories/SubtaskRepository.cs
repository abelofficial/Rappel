using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Infrastructure.Repository;
public class SubtaskRepository<T> : Repository<T> where T : SubTask
{
    public SubtaskRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet
            .Include(st => st.Todo.Project)
            .Where(filterExp)
            .ToListAsync();
    }

}