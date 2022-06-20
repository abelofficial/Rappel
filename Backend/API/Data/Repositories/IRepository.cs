using API.Data.Entities;

namespace API.Data.Repositories;

public interface IRepository<T> where T : IEntity
{
    public Task<T> GetOne(int id);

    public Task<IEnumerable<T>> GetAll();

    public void Create(T t);

    public T Update(T t);

    public Task SaveChanges();
}