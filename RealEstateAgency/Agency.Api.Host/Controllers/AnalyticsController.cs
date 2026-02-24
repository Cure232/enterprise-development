using Agency.Application.Contracts;
using Agency.Application.Contracts.Counterparties;
using Agency.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{

    /// <summary>
    /// Получает список всех продавцов, которые оставили заявки на продажу за указанный период времени
    /// </summary>
    /// <param name="from">Начальная дата периода (включительно)</param>
    /// <param name="to">Конечная дата периода (включительно)</param>
    /// <returns>Список контрагентов-продавцов, оставивших заявки в указанный период</returns>
    /// <response code="200">Успешное получение списка продавцов</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("sellers-with-requests-in-period")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetSellersWithRequestsInPeriod([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        try
        {
            var result = await service.GetSellersWithRequestsInPeriod(from, to);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetSellersWithRequestsInPeriod");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает топ-5 клиентов по количеству заявок на покупку недвижимости
    /// </summary>
    /// <returns>Список пяти клиентов с наибольшим количеством заявок на покупку</returns>
    /// <response code="200">Успешное получение списка топ-5 покупателей</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("top5-buyers-by-request-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetTop5BuyersByRequestCount()
    {
        try
        {
            var result = await service.GetTop5BuyersByRequestCount();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetTop5BuyersByRequestCount");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает топ-5 клиентов по количеству заявок на продажу недвижимости
    /// </summary>
    /// <returns>Список пяти клиентов с наибольшим количеством заявок на продажу</returns>
    /// <response code="200">Успешное получение списка топ-5 продавцов</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("top5-sellers-by-request-count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetTop5SellersByRequestCount()
    {
        try
        {
            var result = await service.GetTop5SellersByRequestCount();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetTop5SellersByRequestCount");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает статистику по количеству заявок для каждого типа недвижимости
    /// </summary>
    /// <returns>Словарь, где ключ - тип недвижимости, значение - количество заявок</returns>
    /// <response code="200">Успешное получение статистики по типам недвижимости</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("request-count-by-property-type")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Dictionary<RealEstateType, int>>> GetRequestCountByPropertyType()
    {
        try
        {
            var result = await service.GetRequestCountByPropertyType();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetRequestCountByPropertyType");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает список клиентов, которые открыли заявки с минимальной стоимостью
    /// </summary>
    /// <returns>Список клиентов, чьи заявки имеют минимальную сумму</returns>
    /// <response code="200">Успешное получение списка клиентов с минимальными заявками</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("clients-with-minimal-request-amount")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetClientsWithMinimalRequestAmount()
    {
        try
        {
            var result = await service.GetClientsWithMinimalRequestAmount();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetClientsWithMinimalRequestAmount");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает список всех клиентов, ищущих недвижимость заданного типа (покупка)
    /// </summary>
    /// <param name="propertyType">Тип недвижимости для поиска</param>
    /// <returns>Список клиентов, ищущих недвижимость указанного типа, отсортированный по ФИО</returns>
    /// <response code="200">Успешное получение списка клиентов по типу недвижимости</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("clients-searching-for-property-type")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetClientsSearchingForPropertyType([FromQuery] RealEstateType propertyType)
    {
        try
        {
            var result = await service.GetClientsSearchingForPropertyType(propertyType);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetClientsSearchingForPropertyType");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Получает список всех клиентов, ищущих дома (тип недвижимости House), отсортированный по ФИО
    /// </summary>
    /// <returns>Список клиентов, ищущих дома, отсортированный по ФИО в алфавитном порядке</returns>
    /// <response code="200">Успешное получение списка клиентов, ищущих дома</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("clients-searching-for-houses-ordered-by-fullname")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CounterpartyDto>>> GetClientsSearchingForHousesOrderedByFullName()
    {
        try
        {
            var result = await service.GetClientsSearchingForHousesOrderedByFullName();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetClientsSearchingForHousesOrderedByFullName");
            return StatusCode(500, ex.Message);
        }
    }
}