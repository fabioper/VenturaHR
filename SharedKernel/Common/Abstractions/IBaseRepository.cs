namespace Common.Abstractions;

public interface IBaseRepository<TEntity, in TId>
    where TEntity : BaseEntity<TId>, IAggregateRoot
    where TId : EntityId
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> GetAll(params ISpecification<TEntity>[] specs);
    Task<TEntity?> FindById(TId id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
}