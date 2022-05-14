namespace Common.Abstractions;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(params ISpecification<T>[] specs);
    Task<T?> FindById(long id);
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
}