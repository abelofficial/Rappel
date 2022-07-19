using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Application;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace API.Infrastructure.Repository;
public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetOne(int id)
    {
        var result = await _dbSet.SingleAsync(e => e.Id == id);
        return result;
    }

    public virtual async Task<T> GetOne(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet.Where(filterExp).SingleAsync();
    }

    public virtual async Task<T> GetOne(Expression<Func<T, bool>> filterExp, string include)
    {
        return await _dbSet.Include(include).Where(filterExp).SingleAsync();
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    public T Create(T t)
    {
        return _dbSet.Add(t).Entity;
    }

    public T Update(T t)
    {
        return _dbSet.Update(t).Entity;
    }

    public virtual async Task<bool> Exists(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet.Where(filterExp).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllWith<I>(Expression<Func<T, bool>> filterExp) where I : IEntity
    {
        return await _dbSet.Include(nameof(I)).Where(filterExp).ToListAsync();
    }

    public virtual async Task<bool> IsTrue(Expression<Func<T, bool>> filterExp)
    {
        return await _dbSet.AnyAsync(filterExp);
    }
}