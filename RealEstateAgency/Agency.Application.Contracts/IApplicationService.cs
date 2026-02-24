namespace Agency.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    Task<TDto> Create(TCreateUpdateDto dto);
    Task<TDto?> Get(TKey dtoId);
    Task<IList<TDto>> GetAll();
    Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);
    Task<bool> Delete(TKey dtoId);
}
