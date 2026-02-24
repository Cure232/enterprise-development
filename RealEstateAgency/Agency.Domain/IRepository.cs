namespace Agency.Domain;

/// <summary>
/// Интерфейс репозитория для CRUD операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    Task<TEntity> Create(TEntity entity);
    Task<TEntity?> Read(TKey entityId);
    Task<IList<TEntity>> ReadAll();
    Task<TEntity> Update(TEntity entity);
    Task<bool> Delete(TKey entityId);
}
