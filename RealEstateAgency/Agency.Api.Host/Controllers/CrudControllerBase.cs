using Agency.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
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
