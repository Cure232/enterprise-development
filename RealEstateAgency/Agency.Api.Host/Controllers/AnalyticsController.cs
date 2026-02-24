using Agency.Application.Contracts;
using Agency.Application.Contracts.Counterparties;
using Agency.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
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
