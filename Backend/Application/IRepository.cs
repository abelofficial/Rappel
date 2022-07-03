using System.Linq.Expressions;
using API.Domain.Entities;

namespace API.Application;

public interface IRepository<T> where T : IEntity
{
    public Task<T> GetOne(int id);

    public Task<T> GetOne(Expression<Func<T, bool>> filterExp);

    public Task<T> GetOne(Expression<Func<T, bool>> filterExp, string? include);

    public Task<IEnumerable<T>> GetAll();

    public T Create(T t);

    public T Update(T t);

    public Task SaveChanges();

    public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filterExp);

    public Task<bool> IsTrue(Expression<Func<T, bool>> filterExp);

    public Task<IEnumerable<T>> GetAllWith<I>(Expression<Func<T, bool>> filterExp) where I : IEntity;
}