using Agency.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

/// <summary>
/// Базовый CRUD контроллер, предоставляющий стандартные операции для работы с сущностями
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения/получения данных сущности</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности (int, Guid, long и т.д.)</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создает новую сущность
    /// </summary>
    /// <param name="newDto">Данные для создания новой сущности</param>
    /// <returns>Созданная сущность с присвоенным идентификатором</returns>
    /// <response code="201">Сущность успешно создана</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{Method} of {Controller} called with {@Dto}", nameof(Create), GetType().Name, newDto);
        try
        {
            var res = await appService.Create(newDto);
            return CreatedAtAction(nameof(Create), res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in {Method} of {Controller}", nameof(Create), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Обновляет существующую сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="newDto">Новые данные для сущности</param>
    /// <returns>Обновленная сущность</returns>
    /// <response code="200">Сущность успешно обновлена</response>
    /// <response code="404">Сущность с указанным идентификатором не найдена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Edit(TKey id, TCreateUpdateDto newDto)
    {
        try
        {
            var res = await appService.Update(newDto, id);
            return Ok(res);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in {Method} of {Controller}", nameof(Edit), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    /// <returns>Статус выполнения операции</returns>
    /// <response code="200">Сущность успешно удалена</response>
    /// <response code="204">Сущность не найдена (ничего не удалено)</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        try
        {
            var res = await appService.Delete(id);
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in {Method} of {Controller}", nameof(Delete), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает список всех сущностей
    /// </summary>
    /// <returns>Список всех сущностей</returns>
    /// <response code="200">Успешное получение списка</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var res = await appService.GetAll();
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in {Method} of {Controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор запрашиваемой сущности</param>
    /// <returns>Сущность с указанным идентификатором</returns>
    /// <response code="200">Сущность найдена</response>
    /// <response code="404">Сущность с указанным идентификатором не найдена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            var res = await appService.Get(id);
            return res == null ? NotFound() : Ok(res);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in {Method} of {Controller}", nameof(Get), GetType().Name);
            return StatusCode(500, ex.Message);
        }
    }
}
