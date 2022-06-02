namespace Common.Abstractions;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> FindById(Guid id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
}