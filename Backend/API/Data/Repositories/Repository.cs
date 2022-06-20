
using API.Data;
using API.Data.Entities;
using API.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetOne(int id)
    {
        try
        {
            var result = await _dbSet.AsNoTracking<T>().SingleAsync(e => e.Id == id);
            return result;
        }
        catch (ArgumentNullException)
        {
            throw new Exception("Key not found");
        }

    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Create(T t)
    {
        _dbSet.Add(t);
    }

    public T Update(T t)
    {
        return _dbSet.Update(t).Entity;
    }
}