using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Infrastructure.Repository;
public class ProjectRepository<T> : Repository<T> where T : Project
{
    public ProjectRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet
         .Include(p => p.Owner)
        .Include(p => p.Items)
        .Include(p => p.Members)
        .Where(filterExp)
        .ToListAsync();
    }

    public override async Task<bool> IsTrue(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet
        .Include(p => p.Owner)
        .Include(p => p.Items)
        .Include(p => p.Members)
        .AnyAsync(filterExp);
    }

}